RMDIR /S /Q ..\binaries
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild ..\..\src\Engine\MvcTurbine.sln -t:Clean
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild ..\..\src\Engine\MvcTurbine.sln -p:Configuration=Debug

xcopy /Y ..\binaries\*.* MVCTurbine\lib
xcopy /Y /S /I ..\..\src\Engine\MvcTurbine\*.cs MvcTurbine\src\MvcTurbine
xcopy /Y /S /I ..\..\src\Engine\MvcTurbine.Web\*.cs MvcTurbine\src\MvcTurbine.Web
nuget pack MVCTurbine/MVCTurbine.nuspec -symbols

xcopy /Y ..\binaries\Ninject\MvcTurbine.Ninject.* MVCTurbine.Ninject\lib
xcopy /Y /S /I ..\..\src\Engine\MvcTurbine.Ninject\*.cs MvcTurbine.Ninject\src
nuget pack MVCTurbine.Ninject/MVCTurbine.Ninject.nuspec -symbols

xcopy /Y ..\binaries\StructureMap\MvcTurbine.StructureMap.* MVCTurbine.StructureMap\lib
xcopy /Y /S /I ..\..\src\Engine\MvcTurbine.StructureMap\*.cs MvcTurbine.StructureMap\src
nuget pack MVCTurbine.StructureMap/MVCTurbine.StructureMap.nuspec -symbols

xcopy /Y ..\binaries\Unity\MvcTurbine.Unity.* MVCTurbine.Unity\lib
xcopy /Y /S /I ..\..\src\Engine\MvcTurbine.Unity\*.cs MvcTurbine.Unity\src
nuget pack MVCTurbine.Unity/MVCTurbine.Unity.nuspec -symbols

xcopy /Y ..\binaries\Windsor\MvcTurbine.Windsor.* MVCTurbine.Windsor\lib
xcopy /Y /S /I ..\..\src\Engine\MvcTurbine.Windsor\*.cs MvcTurbine.Windsor\src
nuget pack MVCTurbine.Windsor/MVCTurbine.Windsor.nuspec -symbols