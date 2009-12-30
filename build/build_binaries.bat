RMDIR /S /Q binaries
%WINDIR%\Microsoft.NET\Framework\v3.5\msbuild ..\src\Engine\MvcTurbine.sln -t:Clean
%WINDIR%\Microsoft.NET\Framework\v3.5\msbuild ..\src\Engine\MvcTurbine.sln -p:Configuration=Release
pause