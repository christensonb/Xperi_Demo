""" This is the start routine for Apache """
__author__ = 'Ben Christenson'
__date__ = "9/11/17"

from .run_flask import run, log

if __name__ == '__main__':
    log.debug("Starting Flask Service from Apache")
    run()