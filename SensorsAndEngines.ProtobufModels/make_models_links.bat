@echo off

if [%~2]==[--Debug] set debug_flag=true

set input_dir=%~1

if [%input_dir%]==[] (
	echo No input directory entered! Use cmd args.
	pause
	exit /b %errorlevel%
)

call :debug_echo "Begin of linking at %input_dir%"
for /D %%G in ("%input_dir%\*") DO ( 
	call :debug_echo "Gonna change the directory"
	cd %%G
	call :debug_echo "I'm at %%G"

	IF EXIST "models.proto" (
		call :debug_echo "Deleted "models.proto" from %%G"
		del "models.proto"
	) 

	IF EXIST "models.options" (
		call :debug_echo "Deleted "models.options" from %%G"
		del "models.options"
	) 

	mklink "models.proto" "..\models.proto"
	call :debug_echo "Made link to "models.proto" inside %%G"

	mklink "models.options" "..\models.options"
	call :debug_echo "Made link to "models.options" inside %%G"

	cd ..
	call :debug_echo "I'm at %%G"
)

:debug_echo
	if [%debug_flag%]==[true] echo %~1
