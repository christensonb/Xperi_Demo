import os
import sys
flask_folder = os.path.abspath(__file__).replace('\\', '/').rsplit('/xperi_demo', 1)[0]
sys.path.append(flask_folder)

from xperi_demo.test.user.user_tests import UserTest
from xperi_demo.test.echo.echo_tests import EchoTest
from xperi_demo.test.account.access_tests import AccessTest
from xperi_demo.test.account.transfer_tests import TransferTest


class AllTest(EchoTest, UserTest, AccessTest, TransferTest):
    """ This is a placeholder to run all tests """
