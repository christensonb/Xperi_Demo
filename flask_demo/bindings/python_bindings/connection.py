from seaborn.rest.intellisense import *
from .account import *
from .echo import *
from .user import *


class Connection(ConnectionEndpoint):
    account = Account()
    echo = Echo()
    user = User()
