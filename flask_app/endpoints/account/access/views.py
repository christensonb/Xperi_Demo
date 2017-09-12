from flask_app.settings.global_import import *

log.trace("Importing endpoint user.views")
from .models import Access
from flask_app.endpoints.account.models import Account

ACCESS = Blueprint('account_access', __name__)


@ACCESS.route('/account/access', methods=['PUT'])
@api_endpoint(auth='User', add=True, commit=True)
def create(account_id, user_id):
    """ Only the primary on the account can add or remove user's access to an account
    :param account_id: int of the account_id for the account
    :param user_id:    int of the user_id to grant access
    :return:           Access dict
    """
    account = Account.query.filter_by(account_id=account_id, user_id=current_user.user_id).first()
    if account is None:
        raise NotFoundException("Account not found, or current user is not the primary on the account")

    access = Access.get_or_create(account_id=account_id, user_id=user_id)
    return access


@ACCESS.route('/account/access', methods=['DELETE'])
@api_endpoint(auth='User', delete=True, commit=True)
def delete(account_id, user_id):
    """ Only the primary on the account can add or remove user's access to an account
    :param account_id: int of the account_id for the account
    :param user_id:    int of the user_id to grant access
    :return:           Access dict
    """
    access = Access.query.filter_by(account_id=account_id, user_id=user_id).first()
    if access is None:
        raise NotFoundException("User %s doesn't have access to account %s" % (user_id, account_id))

    if access.account.user_id != current_user.user_id and current_user.is_auth('Superuser'):
        raise UnauthorizedException("Only primary account holders can delete user's access to an account")

    return access


@ACCESS.route('/account/access/array', methods=['GET'])
@api_endpoint(auth='User')
def get(account_id):
    """ This will return all users who have access to the account, only the primary can do this command
    :param account_id: int of the account_id for the account
    :return:           list of User dict
    """
    account = Account.get(account_id)
    return list(account.users)
