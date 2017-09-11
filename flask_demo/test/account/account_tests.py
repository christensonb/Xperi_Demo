import os
import sys

flask_folder = os.path.abspath(__file__).replace('\\', '/').rsplit('/test', 1)[0]
sys.path.append(flask_folder)

from test.base import *


class AccountTest(BaseTest):
    def test_create_account(self, username="Demo-User", account_name="demo_account"):
        user = self.test_login(username)
        account = user.account.put(name=account_name)
        self.assertEqual(account['name'], account_name, 'Failed to set name')
        self.assertEqual(account['user_id'], user.user_id, 'Failed to set user_id')
        self.assertEqual(account['user_ids'], [user.user_id], 'Failed to set access users')
        return user, account

    def test_get_account(self):
        user, account = self.test_create_account()
        account2 = user.account.get(account['account_id'])
        self.assertEqual(account, account2)

    def test_get_accounts(self):
        user, account = self.test_create_account()
        accounts = user.account.array.get()
        self.assertIn(account, accounts)
