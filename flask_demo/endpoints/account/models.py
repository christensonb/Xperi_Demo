from settings.global_import import *
from endpoints.user.models import User

log.trace("Importing endpoint account.models")

ACCOUNT_STATUS_ENUM = ['Active',
                       'Locked',
                       'Suspended',
                       'Closed']

GENERIC_REGEX = re.compile(r'^[A-Za-z0-9_]{2,30}$')


class Account(db.Model, ApiModel):
    __tablename__ = "account"
    account_id = db.Column(db.Integer, primary_key=True)
    user_id = db.Column(db.Integer, db.ForeignKey('usr.user_id'))
    name = db.Column(db.String, default=datetime_to_str)
    funds = db.Column(db.Float, default=0.0)
    status = db.Column(db.Enum(*ACCOUNT_STATUS_ENUM, name='account_status'), default=ACCOUNT_STATUS_ENUM[0])
    created_timestamp = db.Column(db.DateTime(timezone=True), default=cst_now)

    users = association_proxy('account_access', 'user')

    @classmethod
    def keys(cls):
        return ['name', 'funds', 'user_ids']

    @property
    def user_ids(self):
        """
        :return: list of int of users who are authorized on this account
        """
        return [user.user_id for user in self.users]

    @property
    def is_active(self):
        return self.status == 'Active'

    @classmethod
    def validator_name(cls, username, **kwargs):
        """ This will determine if the user name is valid"""
        assert GENERIC_REGEX.match(username), 'Invalid Account name: %s' % username

    @classmethod
    def validate_status(cls, status, **kwargs):
        assert status in ACCOUNT_STATUS_ENUM, "Account status is not valid"
