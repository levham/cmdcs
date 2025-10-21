	@echo off
	csc -out:m.exe m.cs Core\*.cs

	if %errorlevel% == 1 pause
	if %errorlevel% == 0 pause && start m.exe