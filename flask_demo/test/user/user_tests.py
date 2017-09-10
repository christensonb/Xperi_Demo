__author__ = "Ben Christenson"
__date__ = "9/11/17"

import os
import sys
flask_folder = os.path.abspath(__file__).replace('\\','/').rsplit('/test',1)[0]
sys.path.append(flask_folder)

from test.base import *


class UserTest(BaseTest):
    def test_user_signup(self, username="Ben", password=None, email=None, delete_if_exists=True):
        if password is None:
            password = self.local_data.user_password

        if delete_if_exists:
            try:
                old = self.conn.user.get(username=username)
                self.conn.user.delete(old['user_id'])
            except Exception as e:
                pass
        user_conn = Connection(username, password, base_uri=self.SERVER, timeout=self.TIMEOUT,
                               proxies=self.PROXIES if self.SERVER not in [PROXY_DEBUG_SERVER, PROXY_AWS_SERVER] else None)
        ret = user_conn.user.signup.put(username=username, password=password)

        user_conn.user_id = ret['user_id']
        user_conn._status = "logged in from signup"
        return user_conn

    def test_user_update_email(self, email='new@mechanicsofplay.com'):
        Ben = self.test_user_signup()
        user = Ben.user.post(email=email)
        assert user['email'] == email
        user = Ben.user.get()
        assert user['email'] == email
        Ben.user.login.email.post(email, Ben._password)

    def test_user_update_username(self, username='Ben_New'):
        Ben = self.test_user_signup()
        user = Ben.user.post(username=username)
        assert user['username'] == username
        user = Ben.user.get()
        assert user['username'] == username
        Ben.user.login.post(username, Ben._password)

    def test_user_update_password(self, password='password'):
        Ben = self.test_user_signup('PasswordUpdate','old_password')
        user = Ben.user.post(email="wth@test.com", password=password)
        Ben.user.login.post(user['username'], password)
