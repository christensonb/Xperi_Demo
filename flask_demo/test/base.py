""" This defines a Base Test
"""


__author__ = "Ben Christenson"
__date__ = "9/11/17"

import os
import sys
flask_folder = os.path.abspath(__file__).replace('\\','/').rsplit('/test',1)[0]
sys.path.append(flask_folder)

from settings.config import configuration
from bindings.python_bindings import Connection

from seaborn.file import relative_path, find_file
from seaborn.test.standard_import import *
from test_chain import TestChain

PROXY_DEBUG_SERVER = 'http://127.0.0.1:4777'
PROXY_AWS_SERVER = 'http://127.0.0.1:4888'
DEBUG_SERVER = 'http://127.0.0.1:4999'
AWS_SERVER = 'http://%s'
AWS_SERVER_SSL = 'https://api.puzzlesandpotions.com'


class BaseTest(TestChain):
    thread = None
    SERVER = DEBUG_SERVER
    TIMEOUT = 200
    START_TIME = time.time()
    local_data = LocalData(find_file('_test.json'), no_question=True)
    configuration = configuration

    @classmethod
    def setUpClass(cls):
        traceback_skip_path('/bindings/')
        print("Connecting to Server: %s"%cls.SERVER)
        admin_password = cls.local_data.admin_password
        cls.conn = Connection('Admin-User', admin_password, 'user/login', base_uri=cls.SERVER)
        cls.anonymous = Connection("Anonymous", base_uri=cls.SERVER)

        if cls.SERVER is DEBUG_SERVER or cls.SERVER is PROXY_DEBUG_SERVER:
            try:
                 cls.conn.echo.get()
                 print("Server is Already Started")
            except:
                if sys.version_info[0] == 2:
                    cls._spawn_gevent_flask()
                else:
                    cls._spawn_thread_flask()

    @classmethod
    def _get_flask_run(cls):
        from settings.config import TestConfig
        os.environ['flask_config'] = TestConfig.__name__
        from settings.global_import import setup_flask
        return setup_flask()

    @classmethod
    def _spawn_gevent_flask(cls):
        import gevent
        run = cls._get_flask_run()
        sigquit = getattr(signal, 'SIGQUIT', "Windows_Problem")
        if sigquit != "Windows_Problem":
            gevent.signal(sigquit, gevent.kill)
        cls.thread = gevent.spawn(run)
        cls.thread.start()
        gevent.sleep(0)

    @classmethod
    def _spawn_thread_flask(cls):
        import _thread
        run = cls._get_flask_run()
        _thread.start_new_thread(run,())

    @classmethod
    def tearDownClass(cls):
        print("\n\nThat's All Folks in %s seconds" % round(time.time() - cls.START_TIME, 2))

    def test_login(self, name='Demo-User', password = None):
        user = Connection(name, password or self.local_data.user_password, 'user/login', base_uri=self.SERVER,
                          timeout=self.TIMEOUT)
        return user




