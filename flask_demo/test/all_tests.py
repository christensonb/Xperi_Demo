import os
import sys
flask_folder = os.path.abspath(__file__).replace('\\', '/').rsplit('/test', 1)[0]
sys.path.append(flask_folder)

from test.user.user_tests import UserTest
from test.echo.echo_tests import EchoTest
from test.account.access_tests import AccessTest
from test.account.transfer_tests import TransferTest


class AllTest(EchoTest, UserTest, AccessTest, TransferTest):
    """ This is a placeholder to run all tests """
