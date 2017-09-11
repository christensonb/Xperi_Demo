from settings.global_import import *

log.trace("Importing endpoint account.views")
from .models import Account
from endpoints.account.access.models import Access

ACCOUNT = Blueprint('account', __name__)


@ACCOUNT.route('/account', methods=['GET'])
@api_endpoint(auth='Superuser')
def get(account_id):
    """
    :param account_id: int of the account to get
    :return: Account dict of the account
    """
    account = Account.get(account_id)
    return account


@ACCOUNT.route('/account/array', methods=['GET'])
@api_endpoint(auth='User')
def get_array():
    """
    :return: list of Account dict the current user has access to
    """
    accounts = Account.query.join("Access").filter(user_id=current_user.user_id).all()
    return accounts


@ACCOUNT.route('/account', methods=['PUT'])
@api_endpoint(auth='User', validator=Account, add=True, commit=True)
def create(name=None, user_ids=None):
    """
    :param name:     str of name for the account, defaults to the created timestamp
    :param user_ids: list of int of users to give access to this account defaults to current user
    :return:         Account dict created
    """
    account = Account(name=name or datetime_to_str())
    account.user_access = [Access(user_id=user_id) for user_id in (user_ids or [current_user.user_id])]
    return account
