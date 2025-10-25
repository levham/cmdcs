	@echo off
	csc -out:m.exe m.cs core\*.cs
pause
	if %errorlevel% == 1 pause
	if %errorlevel% == 0 pause && start m.exe