:: When a command line application is launched from Explorer, etc.,
:: and it takes a moment to finish, sending it to this file stops 
:: the console from disappearing by calling pause at the end.
@echo off
%*
pause