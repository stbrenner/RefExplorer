@echo off
%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe Configuration.msbuild /p:Configuration=Release
pause