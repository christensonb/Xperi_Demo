import os
import sys
flask_folder = os.path.abspath(__file__).replace('\\', '/').rsplit('/flask_app', 1)[0]
sys.path.append(flask_folder)

from flask_app.test.user.user_tests import UserTest
from flask_app.test.echo.echo_tests import EchoTest
from flask_app.test.account.access_tests import AccessTest
from flask_app.test.account.transfer_tests import TransferTest


class AllTest(EchoTest, UserTest, AccessTest, TransferTest):
    """ This is a placeholder to run all tests """
