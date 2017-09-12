from seaborn.rest.intellisense import *


class Account_Transfer_Admin_Array(Endpoint):

    def get(self, limit=None, offset=None):
        """
        :param offset:         int of the offset to use
        :param limit:          int of max number of puzzles to return
        :return:               list of Transfer dict
        """
        return self.connection.get('account/transfer/admin/array', limit=limit, offset=offset)


class Account_Transfer_Withdraw(Endpoint):

    def put(self, withdraw_acount_id, amount):
        """
        :param withdraw_acount_id: int of the account_id to withdraw the money from
        :param amount:             float of the amount to transfer
        :return:                   Transfer dict
        """
        return self.connection.put('account/transfer/withdraw', data=dict(withdraw_acount_id=withdraw_acount_id,           amount=amount))


class Account_Transfer_Deposit(Endpoint):

    def put(self, deposit_account_id, amount, receipt):
        """
        :param deposit_account_id: int of the account_id to deposit the moeny to
        :param amount:             float of the amount to transfer
        :param receipt:            str of the validated receipt that money has been received
        :return:                   Transfer dict
        """
        return self.connection.put('account/transfer/deposit',
                                   data=dict(deposit_account_id=deposit_account_id,
                                             amount=amount,
                                             receipt=receipt))


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


class Account_Transfer_Claim(Endpoint):

    def put(self, transfer_id, amount, created_timestamp, receipt):
        """
        :param transfer_id:        int of the account_id to deposit the moeny to
        :param amount:             float of the amount to transfer
        :param created_timestamp:  str of the validated receipt that money has been received
        :param receipt:            str of the receipt
        :return:                   Transfer dict
        """
        return self.connection.put('account/transfer/claim',
                                   data=dict(transfer_id=transfer_id,
                                             amount=amount,
                                             created_timestamp=created_timestamp,
                                             receipt=receipt))


class Account_Transfer_Admin(Endpoint):
    array = Account_Transfer_Admin_Array()


class Account_Transfer(Endpoint):
    admin = Account_Transfer_Admin()
    array = Account_Transfer_Array()
    claim = Account_Transfer_Claim()
    deposit = Account_Transfer_Deposit()
    withdraw = Account_Transfer_Withdraw()

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
