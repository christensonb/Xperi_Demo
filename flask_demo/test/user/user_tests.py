__author__ = "Ben Christenson"
__date__ = "9/11/17"

import os
import sys

flask_folder = os.path.abspath(__file__).replace('\\', '/').rsplit('/test', 1)[0]
sys.path.append(flask_folder)

from test.base import *


class UserTest(BaseTest):
    def test_user_signup(self, username="Ben", password=None, email=None, delete_if_exists=True):
        password = password or self.local_data.user_password

        if delete_if_exists:
            try:
                old = self.conn.user.get(username=username)
                self.conn.user.delete(old['user_id'])
            except Exception as e:
                pass

        user_conn = Connection(username, password, base_uri=self.SERVER, timeout=self.TIMEOUT)
        ret = user_conn.user.signup.put(username=username, password=password, email=email)

        user_conn.user_id = ret['user_id']
        user_conn._status = "logged in from signup"
        return user_conn

    def test_user_update_email(self, email=None):
        email = email or 'update@%s' % self.configuration.domain
        ben = self.test_user_signup()
        user = ben.user.post(email=email)
        self.assertEqual(user['email'], email, 'Failed to set the email')
        user = ben.user.get()
        self.assertEqual(user['email'], email, 'Failed to get the email')
        ben.user.login.email.post(email, ben._password)

    def test_user_update_username(self, username='Ben_New'):
        Ben = self.test_user_signup()
        user = Ben.user.post(username=username)
        self.assertEqual(user['username'], username, 'Failed to set the username')
        user = Ben.user.get()
        self.assertEqual(user['username'], username, 'Failed to get the username')
        Ben.user.login.post(username, Ben._password)

    def test_user_update_password(self, password='password'):
        conn = self.test_user_signup('PasswordUpdate', 'old_password')
        user = conn.user.post(email="wth@test.com", password=password)
        conn.user.login.post(user['username'], password)

    def test_user_logout(self):
        conn = Connection('Demo-User',self.local_data.user_password, base_uri=self.SERVER, timeout=self.TIMEOUT,
                               login_url='user/login')
        user = conn.user.get()
        self.assertEqual(conn.user_id, user['user_id'], 'Failed to make sure we started out logged in')

        conn.user.logout.post()

        try:
            user = conn.user.get() # this is suppose to fail because we are logged out
        except Exception as ex:
            return

        raise BaseException("Failed to fail at getting user info while logged out")

