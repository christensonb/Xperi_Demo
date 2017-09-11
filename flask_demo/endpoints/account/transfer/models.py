from settings.global_import import *
from endpoints.account.models import Account

log.trace("Importing endpoint account.transfer.models")


class Transfer(db.Model, ApiModel):
    __tablename__ = "account_transfer"
    transfer_id = db.Column(db.Integer, primary_key=True)
    user_id = db.Column(db.Integer, db.ForeignKey('account.account_id'))
    deposit_account_id = db.Column(db.Integer, db.ForeignKey('account.account_id'))
    withdraw_account_id = db.Column(db.Integer, db.ForeignKey('account.account_id'))
    amount = db.Column(db.Float)
    created_timestamp = db.Column(db.DateTime(timezone=True), default=cst_now)

    deposit_account = db.relationship(Account, foreign_keys='Transfer.deposit_account_id',
                                      backref=backref('account_deposits', lazy='dynamic', uselist=True))

    withdraw_account = db.relationship(Account, foreign_keys='Transfer.withdraw_account_id',
                                       backref=backref('account_withdraws', lazy='dynamic', uselist=True))

    @classmethod
    def keys(cls):
        return ['user_id', 'deposit_account_id', 'deposit_account_name', 'withdraw_account_id', 'withdraw_account_name',
                'amount', 'created_timestamp']

    @property
    def deposit_account_name(self):
        """
        :return: str of the name of the account
        """
        return self.deposit_account.name

    @property
    def withdraw_account_name(self):
        """
        :return: str of the name of the account
        """
        return self.withdraw_account.name

    def validate_amount(self, amount, **kwargs):
        """ This will validate amount is positive and less than 1000000 """
        assert amount > 0, "Transfer amounts have to be greater than zero"
        assert amount <= 1000000, "Transfer amounts have to be less than a million dollars"
