@echo off
REM %~d1
REM cd %~dp1


SETLOCAL

if [%1]==[] goto usage

for /f "delims=" %%i in ("%~1") do set MYPATH="%%~fi"
pushd %MYPATH% 2>nul

if errorlevel 1 goto notdir
goto isdir

:notdir
cd /d %~dp1
goto exit

:isdir
popd
cd /d %1
goto exit

:usage
echo Usage:  %0 DIRECTORY_TO_TEST
exit /b

:exit


start "" powershell


ENDLOCAL