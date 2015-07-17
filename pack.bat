@ECHO OFF
@ECHO ------------------------
@ECHO GIT TAG
@ECHO ------------------------
@ECHO.
 
IF "%1" == "" GOTO SyntaxError
 
REM main code
git tag -a "v%1" -m "%1"

@ECHO.
@ECHO Push to GitHub
git push origin master
git push origin --tags
@ECHO.

GOTO End
 
:SyntaxError
@ECHO Usage:
@ECHO.
@ECHO     nugetpack version
@ECHO.
@ECHO Sample:
@ECHO. 
@ECHO     nugetpack 0.1.2
@ECHO.
@ECHO     will create tag 'v0.1.2' 
@ECHO     and release and nuget package version '0.1.2'
GOTO End
 
:End
@ECHO ON