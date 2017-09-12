from setuptools import setup, find_packages

plugins = []
setup(
    name='XperiDemo',
    version='0.0.1',
    description='This is a banking demo, implemented using flask and test-chain',
    long_description='',
    author='Ben Christenson',
    author_email='Python@BenChristenson.com',
    url='',
    install_requires=["test-chain>=0.0.1","seaborn>=0.2.1"],
    packages=find_packages(exclude=()),
    license='MIT License',
    classifiers=(
        'Development Status :: 1 - Beta',
        'Intended Audience :: Developers',
        'Natural Language :: English',
        'License :: Other/Proprietary License',
        'Operating System :: POSIX :: Linux',
        'Programming Language :: Python',
        'Programming Language :: Python :: 2.7',
        'Programming Language :: Python :: 3.5'))
