from flask_app.settings.global_import import *
from flask_app.endpoints.user.models import User
from flask_app.endpoints.account.models import Account

log.trace("Importing endpoint account.access.models")


class Access(db.Model, ApiModel):
    __tablename__ = "account_access"
    access_id = db.Column(db.Integer, primary_key=True)
    account_id = db.Column(db.Integer, db.ForeignKey('account.account_id'))
    user_id = db.Column(db.Integer, db.ForeignKey('usr.user_id'))

    user = db.relationship(User, backref=backref('account_access', lazy='dynamic', uselist=True))
    account = db.relationship(Account, backref=backref('user_access', lazy='dynamic', uselist=True))
