from settings.global_import import *

log.trace("Importing endpoint account.transfer.views")
from .models import Transfer
from endpoints.account.models import Account
from endpoints.account.access.models import Access

TRANSFER = Blueprint('transfer', __name__)


@TRANSFER.route('/account/transfer', methods=['GET'])
@api_endpoint(auth='User')
def get(transfer_id):
    """
    :param transfer_id: int of the id for the transfer
    :return: Transfer dict
    """
    transfer = Transfer.get(transfer_id)
    if not (current_user.is_auth('Superuser') or
                    current_user in transfer.withdraw_account.users or
                    current_user in transfer.deposit_account.users):
        raise UnauthorizedException("User is not a member of the accounts effected by this transfer")
    return transfer


@TRANSFER.route('/account/transfer/array', methods=['GET'])
@api_endpoint(auth='User')
def get_array(account_id, withdraws_only=None, limit=None, offset=None):
    """
    :param account_id:     int of the account_id to get transfer for
    :param withdraws_only: bool if true only gets withdraw transfer if false only gets deposit, default gets both
    :param offset:         int of the offset to use
    :param limit:          int of max number of puzzles to return
    :return:               list of Transfer dict
    """
    query = Transfer.query

    if withdraws_only is True:
        query.filter(withdraw_account_id=account_id)
    if withdraws_only is False:
        query.filter(deposit_account_id=account_id)
    if withdraws_only is None:
        query.filter(or_(withdraw_account_id=account_id,
                         deposit_Account_id=account_id))

    if offset is not None and offset < 0:
        offset += query.count()

    if offset:
        query = query.offset(offset)

    if limit:
        query = query.limit(limit)

    transfers = query.all()
    return transfers


@TRANSFER.route('/account/transfer', methods=['PUT'])
@api_endpoint(auth='User', validator=Transfer, add=True, commit=True)
def create(withdraw_acount_id, deposit_account_id, amount):
    """
    :param withdraw_acount_id: int of the account_id to withdraw the money from
    :param deposit_account_id: int of the account_id to deposit the moeny to
    :param amount:             float of the amount to transfer
    :return:                   Transfer dict
    """
    query = Account.query.filter(account_id=withdraw_acount_id)
    query = query.join(Access).filter_by(Access.user_id == current_user.user_id)
    withdraw_account = query.first()
    if withdraw_account is None:
        raise UnauthorizedException("Current User does not have access to the account")

    if withdraw_account.funds < amount:
        raise PaymentRequiredException("Insufficent funds to do the transfer")

    if not withdraw_account.is_active:
        raise UnauthorizedException("The withdraw account is not active")

    deposit_account = Account.query.filter(account_id=deposit_account_id)

    if deposit_account is None:
        raise NotFoundException("Failed to find deposit account")

    if not deposit_account.is_active:
        raise BadRequestException("The deposit account is not active")

    deposit_account.funds += amount
    withdraw_account.funds -= amount

    transfer = Transfer(user_id=current_user.user_id, amount=amount)
    transfer.deposit_account = deposit_account
    transfer.withdraw_account = withdraw_account
    return transfer


@TRANSFER.route('/account/transfer/withdraw', methods=['PUT'])
@api_endpoint(auth='User', validator=Transfer, add=True, commit=True)
def create_withdraw(withdraw_acount_id, amount):
    """
    :param withdraw_acount_id: int of the account_id to withdraw the money from
    :param amount:             float of the amount to transfer
    :return:                   Transfer dict
    """
    query = Account.query.filter(account_id=withdraw_acount_id)
    query = query.join(Access).filter_by(Access.user_id == current_user.user_id)
    withdraw_account = query.first()
    if withdraw_account is None:
        raise UnauthorizedException("Current User does not have access to the account")

    if withdraw_account.funds < amount:
        raise PaymentRequiredException("Insufficent funds to do the transfer")

    if not withdraw_account.is_active:
        raise UnauthorizedException("The withdraw account is not active")

    withdraw_account.funds -= amount

    transfer = Transfer(user_id=current_user.user_id, amount=amount)
    transfer.withdraw_account = withdraw_account
    transfer.generate_receipt()
    return transfer


@TRANSFER.route('/account/transfer/deposit', methods=['PUT'])
@api_endpoint(auth='User', validator=Transfer, add=True, commit=True)
def create_deposit(deposit_account_id, amount, deposit_receipt):
    """
    :param deposit_account_id: int of the account_id to deposit the moeny to
    :param amount:             float of the amount to transfer
    :param deposit_receipt:    str of the validated receipt that money has been received
    :return:                   Transfer dict
    """
    deposit_account = Account.query.filter(account_id=deposit_account_id)

    if deposit_account is None:
        raise NotFoundException("Failed to find deposit account")

    if not deposit_account.is_active:
        raise BadRequestException("The deposit account is not active")

    transfer = Transfer(user_id=current_user.user_id, amount=amount)
    if not transfer.check_receipt(deposit_receipt):
        raise UnauthorizedException("Deposit Receipt is invalid")

    deposit_account.funds += amount
    transfer.deposit_account = deposit_account
    return transfer


@TRANSFER.route('/account/transfer/claim', methods=['PUT'])
@api_endpoint(auth='Admin', validator=Transfer, add=True, commit=True)
def claim(transfer_id, amount, created_timestamp, receipt):
    """
    :param transfer_id:        int of the account_id to deposit the moeny to
    :param amount:             float of the amount to transfer
    :param created_timestamp:  str of the validated receipt that money has been received
    :param receipt:            str of the receipt
    :return:                   Transfer dict
    """
    transfer = Transfer.query.filter(transfer_id).first()
    if transfer is None:
        raise NotFoundException("Failed to find transfer: %s" % transfer_id)

    if transfer.amount != amount or transfer.created_timestamp != created_timestamp:
        raise BadRequestException("Transfer parameters did not match amount or timestamp")

    if transfer.receipt != receipt:
        raise UnauthorizedException("Transfer receipt did not match")

    transfer.receipt = ""
    return transfer
