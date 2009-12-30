mkdir bin
tools\wix\candle.exe -out bin\turbine.wixobj msi_template.wxs
tools\wix\light.exe -out bin\MVCTurbine.msi bin\turbine.wixobj
pause