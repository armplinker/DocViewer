@ECHO OFF
ECHO Per­form­ing IIS Reset
IISRESET
ECHO Delet­ing Cache
Del /F /Q /S %LOCALAPPDATA%\Microsoft\WebsiteCache\*.*
Del /F /Q /S %LOCALAPPDATA%\Temp\VWDWebCache\*.*
Del /F /Q /S "C:LOCALAPPDATA%\Microsoft\Team Foundation\3.0\Cache\*.*"
Del /F /Q /S "C:\Windows\Microsoft.NET\Framework\v2.0.50727\Temporary ASP.NET Files\*.*"
Del /F /Q /S "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Temporary ASP.NET Files\*.*"
Del /F /Q /S "C:\Windows\Microsoft.NET\Framework64\v2.0.50727\Temporary ASP.NET Files\*.*"
Del /F /Q /S "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Temporary ASP.NET Files\*.*"
ECHO Complete