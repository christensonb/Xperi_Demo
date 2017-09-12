"""
    This is a Command Line Interpreter to act as an interface to teh flask demo.

    Simply run the script with python and choose from the list of options.
"""
import os
import platform
from six.moves import input
from seaborn.table import SeabornTable
from test.base import *


class CommandLineInterpretorDemo(object):
    SERVER = None
    local_data = LocalData(find_file('_test.json'), no_question=True)

    def __init__(self):
        self.anoymous = None
        self.conn = None

    def clear(self):
        if 'Windows' in platform.platform:
            os.popen('clear').read()
        else:
            os.popen('cls').read()

    def print_options(self, options, message, padding=5):
        """ This will print the menu options and return the users choice
        :param options: list of string of options to be printed as a list with numbers
        :param message: str of message to print with the input
        :return: str or int of the users input
        """
        print("\n" * padding)
        for i, option in enumerate(options):
            print("%s: %s" % (str(i).ljust(3), option))
        ret = input(message + ': ')
        if ret.isdigit():
            return eval(ret)
        return ret

    def print_table(self, title, data, clear=True, padding=5):
        """  This will print the results from the api calls as a table
        :param title: str of the message as a title
        :param data: obj of the data from the api call
        :param clear: bool if true clears the screen before printing
        :param padding: int of the number of line breaks after themessage
        :return: None
        """
        if isinstance(data, dict):
            data = [data]
        if clear:
            self.clear()
        print(title)
        print(SeabornTable(data))
        print('\n' * padding)

    def setup_server(self):
        """ This will setup the server to talk to and if needed start the server """
        self.clear()
        answer = 0  # self.print_options(['Local Server', 'Remote Server'], "Which Server do you want to start")
        if answer == 0:
            self.SERVER = DEBUG_SERVER
            print("Connecting to local server: %s" % self.SERVER)
        else:
            self.SERVER = AWS_SERVER
            print("Connecting to remote server: %s" % self.SERVER)

        self.anonymous = Connection("Anonymous", base_uri=self.SERVER)

        try:
            self.anonymous.echo.get()
            print("Server was already up and running")
        except:
            print("Local Server being started automatically")
            from settings.global_import import setup_flask
            import endpoints
            run = setup_flask.setup_run(endpoints)
            thread.start_new_thread(run, ())

        self.admin = Connection('Admin-User', self.local_data.admin_password, 'user/login', base_uri=self.SERVER)
        self.conn = self.admin

    def run(self):
        """ This is the main loop of the program that executes the user's menu choices """
        self.setup_server()
        options = ["Quit", "Login as User", "Create User", "List All Users",
                   "Create Account", "List All Accounts", "List User Accounts",
                   "Create Access", "List Account Access",
                   "Create Transfer", "List All Transfers", "List Account Transfers"]

        while (True):
            answer = self.print_options(options, "What do you want to do")
            if isinstance(answer, int) and 0 <= answer and answer < len(options):
                func = options[answer].replace(' ', '_').lower()
                try:
                    data = getattr(self, func)()
                    if data is not None:
                        self.print_table(options[answer], data)
                except Exception as ex:
                    print(str(ex))

    def quit(self):
        sys.exit()

    def login_as_user(self):
        user = self.conn.login(input('username: '), input('password: '), login_url='user/login')
        self.print_table("Login User", user)

    def create_user(self):
        username = input('username: ')
        password = input('password: ')
        self.conn = Connection(username, base_uri=self.SERVER)
        return self.conn.user.signup.post(username, password)

    def list_all_users(self):
        return self.admin.user.array.get()

    def create_account(self):
        return self.conn.account.put(input("name: "))

    def list_all_accounts(self):
        return self.admin.account.admin.array.get()

    def list_user_accounts(self):
        return self.conn.account.array.get()

    def create_access(self):
        accounts = self.conn.account.array.get(primary=True)
        self.print_table("Users accounts pick one", accounts)
        return self.conn.account.access.put(input("account_id: "),
                                            input("user_id: "))

    def list_all_access(self):
        return self.admin.account.access.get(input("account_id: "))

    def create_transfer(self):
        accounts = self.admin.account.admin.array.get()
        self.print_table("UserID: %s" % self.conn.user_id, accounts)
        withdraw_account_id = input("withdraw_account_id: ")
        deposit_account_id = input("deposit_account_id: ")
        amount = input("amount to transfer: ")
        transfer = self.conn.transfer.put(withdraw_account_id or None, deposit_account_id or None, amount)
        self.print_table("Create Transfer", transfer)

        accounts = []
        if withdraw_account_id:
            accounts.append(self.admin.account.get(withdraw_account_id))
        if deposit_account_id:
            accounts.append(self.admin.account.get(deposit_account_id))
        self.print_table("Account Balance", accounts, clear=False)
        return None

    def list_all_transfers(self):
        return self.admin.account.transfer.admin.array.get(limit=30, offset=-30)

    def list_account_transfers(self):
        return self.conn.account.transfer.array.get()


if __name__ == '__main__':
    CommandLineInterpretorDemo().run()
