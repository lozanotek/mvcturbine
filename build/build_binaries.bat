RMDIR /S /Q binaries
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild ..\..\src\Engine\MvcTurbine.sln -t:Clean
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild ..\..\src\Engine\MvcTurbine.sln -p:Configuration=Release
