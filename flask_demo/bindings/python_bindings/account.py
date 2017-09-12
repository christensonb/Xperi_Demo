from .account_transfer import *
from .account_access import *
from seaborn.rest.intellisense import *


class Account_Admin_Array(Endpoint):

    def get(self, limit=None, offset=None):
        """
        :param offset: int of the offset to use
        :param limit:  int of max number of puzzles to return
        :return:       list of Account dict the current user has access to
        """
        return self.connection.get('account/admin/array', limit=limit, offset=offset)


class Account_Array(Endpoint):

    def get(self, primary=False, limit=None, offset=None):
        """
        :param primary: bool if True will only reutnr accounts the user is primary on
        :param offset: int of the offset to use
        :param limit:  int of max number of puzzles to return
        :return:       list of Account dict the current user has access to
        """
        return self.connection.get('account/array',
                                   primary=primary,
                                   limit=limit,
                                   offset=offset)


class Account_Admin(Endpoint):
    array = Account_Admin_Array()


class Account(Endpoint):
    access = Account_Access()
    admin = Account_Admin()
    array = Account_Array()
    transfer = Account_Transfer()

    def get(self, account_id):
        """
        :param account_id: int of the account to get
        :return: Account dict of the account
        """
        return self.connection.get('account', account_id=account_id)

    def put(self, name=None, user_ids=None):
        """
        :param name:     str of name for the account, defaults to the created timestamp
        :param user_ids: list of int of users to give access to this account defaults to current user
        :return:         Account dict created
        """
        return self.connection.put('account', data=dict(name=name,           user_ids=user_ids))
