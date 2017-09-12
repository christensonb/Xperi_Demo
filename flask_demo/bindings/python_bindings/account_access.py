from seaborn.rest.intellisense import *


class Account_Access_Array(Endpoint):

    def get(self, account_id):
        """ This will return all users who have access to the account, only the primary can do this command
        :param account_id: int of the account_id for the account
        :return:           list of User dict
        """
        return self.connection.get('account/access/array', account_id=account_id)


class Account_Access(Endpoint):
    array = Account_Access_Array()

    def put(self, account_id, user_id):
        """ Only the primary on the account can add or remove user's access to an account
        :param account_id: int of the account_id for the account
        :param user_id:    int of the user_id to grant access
        :return:           Access dict
        """
        return self.connection.put('account/access', data=dict(account_id=account_id,           user_id=user_id))

    def delete(self, account_id, user_id):
        """ Only the primary on the account can add or remove user's access to an account
        :param account_id: int of the account_id for the account
        :param user_id:    int of the user_id to grant access
        :return:           Access dict
        """
        return self.connection.delete('account/access', account_id=account_id, user_id=user_id)
