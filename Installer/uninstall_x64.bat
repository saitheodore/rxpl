cd /d %~dp0
net stop "RXPL Account Services"
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe -u RXPL.Services.Host.exe
pause