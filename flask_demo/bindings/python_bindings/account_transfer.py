from seaborn.rest.intellisense import *


class Account_Transfer_Array(Endpoint):

    def get(self, account_id, withdraws_only=None, limit=None, offset=None):
        """
        :param account_id:     int of the account_id to get transfer for
        :param withdraws_only: bool if true only gets withdraw transfer if false only gets deposit, default gets both
        :param offset:         int of the offset to use
        :param limit:          int of max number of puzzles to return
        :return:               list of Transfer dict
        """
        return self.connection.get('account/transfer/array',
                                   account_id=account_id,
                                   withdraws_only=withdraws_only,
                                   limit=limit,
                                   offset=offset)


class Account_Transfer(Endpoint):
    array = Account_Transfer_Array()

    def get(self, transfer_id):
        """
        :param transfer_id: int of the id for the transfer
        :return: Transfer dict
        """
        return self.connection.get('account/transfer', transfer_id=transfer_id)

    def put(self, withdraw_acount_id, deposit_account_id, amount):
        """
        :param withdraw_acount_id: int of the account_id to withdraw the money from
        :param deposit_account_id: int of the account_id to deposit the moeny to
        :param amount:             float of the amount to transfer
        :return:                   Transfer dict
        """
        return self.connection.put('account/transfer',
                                   data=dict(withdraw_acount_id=withdraw_acount_id,
                                             deposit_account_id=deposit_account_id,
                                             amount=amount))
