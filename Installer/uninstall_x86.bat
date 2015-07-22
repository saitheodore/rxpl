cd /d %~dp0
net stop "RXPL Account Services"
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe -u RXPL.Services.Host.exe
pause