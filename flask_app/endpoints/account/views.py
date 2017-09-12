from flask_app.settings.global_import import *

log.trace("Importing endpoint account.views")
from .models import Account
from flask_app.endpoints.account.access.models import Access

ACCOUNT = Blueprint('account', __name__)


@ACCOUNT.route('/account', methods=['GET'])
@api_endpoint(auth='User')
def get(account_id):
    """
    :param account_id: int of the account to get
    :return: Account dict of the account
    """
    account = Account.get(account_id)
    return account


@ACCOUNT.route('/account/admin/array', methods=['GET'])
@api_endpoint(auth='Admin')
def get_all_array(limit=None, offset=None):
    """
    :param offset: int of the offset to use
    :param limit:  int of max number of puzzles to return
    :return:       list of Account dict the current user has access to
    """
    query = Account.query
    if offset is not None and offset < 0:
        offset += query.count()

    if offset:
        query = query.offset(offset)

    if limit:
        query = query.limit(limit)

    accounts = query.all()
    return accounts

@ACCOUNT.route('/account/array', methods=['GET'])
@api_endpoint(auth='User')
def get_array(primary= False, limit=None, offset=None):
    """
    :param primary: bool if True will only return accounts the user is primary on
    :param offset: int of the offset to use
    :param limit:  int of max number of puzzles to return
    :return:       list of Account dict the current user has access to
    """
    query = Account.query
    if primary:
        query = query.filter_by(user_id=current_user.user_id)
    else:
        query = query.join(Access).filter(Access.user_id == current_user.user_id)
    if offset is not None and offset < 0:
        offset += query.count()

    if offset:
        query = query.offset(offset)

    if limit:
        query = query.limit(limit)

    accounts = query.all()
    return accounts


@ACCOUNT.route('/account', methods=['PUT'])
@api_endpoint(auth='User', validator=Account, add=True, commit=True)
def create(name=None, user_ids=None):
    """
    :param name:     str of name for the account, defaults to the created timestamp
    :param user_ids: list of int of users to give access to this account defaults to current user
    :return:         Account dict created
    """
    account =  Account.query.filter_by(name = name).first() if name is not None else None
    user_ids = user_ids or [current_user.user_id]
    if account is None:
        account = Account(name=name or datetime_to_str(), user_id=current_user.user_id)
        account.user_access = [Access(user_id=user_id) for user_id in user_ids]
    else:
        if account.user_id is not current_user.user_id:
            raise UnauthorizedException('Account with the name "%s" already exists'%name)

        for access in account.user_access:
            if access.user_id in user_ids:
                user_ids.remove(access.user_id)
        for user_id in user_ids:
            account.user_access.append(Access(user_id=user_id))
    return account
