"""
    This is a Command Line Interpreter to act as an interface to teh flask demo.

    Simply run the script with python and choose from the list of options.

    The user inputs can be saved to a file using -o filename at command line
    The user inputs can be mocked from a file using -i filename at command line

    Example:
        python cli.py -i test\mock_cli_input.txt -o test\mock_cli_input.txt

"""
import os
from six.moves import input
from seaborn.table import SeabornTable
from test.base import *
from settings.global_import import setup_flask
import endpoints
from logging import log
from seaborn.logger import setup_log_level

setup_log_level("ERROR")


class CommandLineInterpretorDemo(object):
    SERVER = None
    local_data = LocalData(find_file('_test.json'), no_question=True)

    def __init__(self, args):
        self.mock_input = []
        store_file = None

        while len(args) >= 2:
            if args[0] == '-i':
                if os.path.exists(args[1]):
                    self.mock_input = open(args[1], 'r').read().split('\n')
                args = args[2:]
            elif args[0] == '-o':
                store_file = args[1]
                args = args[2:]
            else:
                args = args[1:]

        self.store_input = store_file and open(store_file,'w') or None

        self.anoymous = None
        self.conn = None

    def input(self, question):
        if len(self.mock_input) > 0 and self.mock_input[0] != '' and \
                        self.mock_input[0] != "What do you want to do: 0":
            print(self.mock_input[0])
            answer = self.mock_input.pop(0).split(':', 1)[1]
        else:
            answer = input(question)
        if self.store_input != None:
            self.store_input.write('%s%s\n' % (question, answer))
        return answer

    def clear(self):
        pass
        # os.system('clear')

    def print_options(self, options, message, padding=5):
        """ This will print the menu options and return the users choice
        :param options: list of string of options to be printed as a list with numbers
        :param message: str of message to print with the self.input
        :return: str or int of the users self.input
        """
        print("\n" * padding)
        for i, option in enumerate(options):
            print("%s : %s" % (str(i).rjust(3), option))
        print('')
        ret = self.input(message + ': ')
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
        print(title+'\n')
        print(SeabornTable(data).obj_to_mark_down(True))
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
            run = setup_flask.setup_run(endpoints)
            thread.start_new_thread(run, ())

        self.admin = Connection('Admin-User', self.local_data.admin_password, 'user/login', base_uri=self.SERVER)
        self.conn = self.admin
        self.clear()

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
            else:
                self.clear()

    def quit(self):
        sys.exit()

    def login_as_user(self):
        self.conn.login(self.input('username: '), self.input('password: '), login_url='user/login')
        return self.conn.user.get()

    def create_user(self):
        username = self.input('username: ')
        password = self.input('password: ')
        self.conn = Connection(username, base_uri=self.SERVER)
        return self.conn.user.signup.put(username, password)

    def list_all_users(self):
        return self.admin.user.array.get()

    def create_account(self):
        return self.conn.account.put(self.input("name: "))

    def list_all_accounts(self):
        return self.admin.account.admin.array.get()

    def list_user_accounts(self):
        return self.conn.account.array.get()

    def create_access(self):
        accounts = self.conn.account.array.get(primary=True)
        self.print_table("Users accounts pick one", accounts)
        return self.conn.account.access.put(self.input("account_id: "),
                                            self.input("user_id: "))

    def list_account_access(self):
        return self.admin.account.access.array.get(self.input("account_id: "))

    def create_transfer(self):
        accounts = self.admin.account.admin.array.get()
        self.print_table("UserID: %s" % self.conn.user_id, accounts)
        withdraw_account_id = self.input("withdraw_account_id: ")
        deposit_account_id = self.input("deposit_account_id: ")
        amount = self.input("amount to transfer: ")

        if withdraw_account_id == "":
            transfer = self.conn.account.transfer.deposit.put(deposit_account_id, amount, "")
        elif deposit_account_id == "":
            transfer = self.conn.account.transfer.withdraw.put(withdraw_account_id, amount)
        else:
            transfer = self.conn.account.transfer.put(withdraw_account_id, deposit_account_id, amount)
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
        return self.conn.account.transfer.array.get(self.input("account_id: "))


if __name__ == '__main__':
    CommandLineInterpretorDemo(sys.argv).run()
