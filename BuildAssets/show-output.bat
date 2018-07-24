@echo off
cd /D "%~dp0"
(
    echo Get-Content "$($env:LOCALAPPDATA)Low\DarkLink\VnotifieR\output_log.txt" -Wait -Tail 5
) | powershell.exe -Command -