import os
import sys

flask_folder = os.path.abspath(__file__).replace('\\', '/').rsplit('/test', 1)[0]
sys.path.append(flask_folder)
from test.base import *
from test.account.account_tests import AccountTest


class AccessTest(AccountTest):
    def test_create_access(self, username="Demo-User", account_name="demo_account"):
        user, account = self.test_create_account(username, account_name)

        try:
            other_user = self.user_signup('Demo-User2')
        except Exception as ex:
            raise unittest.SkipTest('Exception in User Signup: %s' % ex)

        try:
            other_user.account.access.put(account['account_id'], other_user.user_id)
        except NotFoundException as ex:
            pass  # only the primary should be grant access to an account

        if other_user.user_id in account['user_ids']:
            user.account.access.delete(account['account_id'], other_user.user_id)

        user.account.access.put(account['account_id'], other_user.user_id)

        account = user.account.get(account['account_id'])  # this is because it will have the new user_id
        self.assertIn(other_user.user_id, account['user_ids'])

        accounts_of_other_user = other_user.account.array.get()
        self.assertIn(account, accounts_of_other_user)

        return user, account, other_user

    def test_get_users_with_account_accesss(self):
        user, account, other_user = self.test_create_access()
        users_with_access = user.account.access.array.get(account['account_id'])
        self.assertIn(user.user_id, [user['user_id'] for user in users_with_access])
        self.assertIn(other_user.user_id, [user['user_id'] for user in users_with_access])

    def test_get_account(self):
        user, account = self.test_create_account()
        account2 = user.account.get(account['account_id'])
        self.assertEqual(account, account2)

    def test_get_accounts(self):
        user, account = self.test_create_account()
        accounts = user.account.array.get()
        self.assertIn(account, accounts)
