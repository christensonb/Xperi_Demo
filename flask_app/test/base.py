""" This defines a Base Test
"""
import os
import sys

if sys.version_info[0] == 3:
    import _thread as thread
else:
    import thread

flask_folder = os.path.abspath(__file__).replace('\\', '/').rsplit('/flask_app', 1)[0]
sys.path.append(flask_folder)

from flask_app.settings.config import configuration
from flask_app.bindings.python_bindings import Connection

from seaborn.file import find_file
from seaborn.test.standard_import import *
from test_chain import TestChain

PROXY_DEBUG_SERVER = 'http://127.0.0.1:4777'
PROXY_AWS_SERVER = 'http://127.0.0.1:4888'
DEBUG_SERVER = 'http://127.0.0.1:4999'
AWS_SERVER = 'http://%s' % configuration.domain
AWS_SERVER_SSL = 'https://%s' % configuration.domain


class BaseTest(TestChain):
    thread = None
    SERVER = DEBUG_SERVER
    TIMEOUT = 10
    START_TIME = time.time()
    local_data = LocalData(find_file('_test.json'), no_question=True)
    configuration = configuration

    @classmethod
    def setUpClass(cls):
        traceback_skip_path('/bindings/')
        print("Connecting to Server: %s" % cls.SERVER)
        admin_password = cls.local_data.admin_password
        cls.anonymous = Connection("Anonymous", base_uri=cls.SERVER)
        if cls.SERVER is DEBUG_SERVER or cls.SERVER is PROXY_DEBUG_SERVER:
            cls.start_server()
        cls.conn = Connection('Admin-User', admin_password, 'user/login', base_uri=cls.SERVER)

    @classmethod
    def start_server(cls):
        try:
            cls.anonymous.echo.get()
            print("Server is Already Started")
        except:
            from flask_app.settings.global_import import setup_flask
            from flask_app import endpoints
            run = setup_flask.setup_run(endpoints)
            thread.start_new_thread(run, ())

    @classmethod
    def tearDownClass(cls):
        print("\n\nThat's All Folks in %s seconds" % round(time.time() - cls.START_TIME, 2))

    def test_login(self, name='Demo-User', password=None):
        user = Connection(name, password or self.local_data.user_password, 'user/login', base_uri=self.SERVER,
                          timeout=self.TIMEOUT)
        return user

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
