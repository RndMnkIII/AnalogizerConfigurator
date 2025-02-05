rd /s /q release
mkdir release


@ECHO *************************
@ECHO *************************
@ECHO *** Windows x64 Build ***
@ECHO *************************
@ECHO *************************
dotnet publish AnalogizerConfigurator.csproj -r win-x64 --self-contained true -c Release -o AnalogizerConfigurator_win_x64 --consoleloggerparameters:ErrorsOnly -p:PublishTrimmed=True -p:TrimMode=partial
move AnalogizerConfigurator_win_x64\AnalogizerConfigurator.exe release\AnalogizerConfigurator_win_x64.exe
rmdir /s /q AnalogizerConfigurator_win_x64
cd release
7z a -tzip "AnalogizerConfigurator_win_x64.zip" "AnalogizerConfigurator_win_x64.exe"
del AnalogizerConfigurator_win_x64.exe
cd ..


@ECHO *************************
@ECHO *************************
@ECHO *** Windows x86 Build ***
@ECHO *************************
@ECHO *************************
dotnet publish AnalogizerConfigurator.csproj -r win-x86 --self-contained true -c Release -o AnalogizerConfigurator_win_x86 --consoleloggerparameters:ErrorsOnly -p:PublishTrimmed=True -p:TrimMode=partial
move AnalogizerConfigurator_win_x86\AnalogizerConfigurator.exe release\AnalogizerConfigurator_win_x86.exe
rmdir /s /q AnalogizerConfigurator_win_x86
cd release
7z	a -tzip "AnalogizerConfigurator_win_x86.zip" "AnalogizerConfigurator_win_x86.exe"
del AnalogizerConfigurator_win_x86.exe
cd ..


@ECHO *************************
@ECHO ***************************
@ECHO *** Windows ARM32 Build ***
@ECHO *************************
@ECHO ***************************
dotnet publish AnalogizerConfigurator.csproj -r win-arm --self-contained true -c Release -o AnalogizerConfigurator_win_arm --consoleloggerparameters:ErrorsOnly -p:PublishTrimmed=True -p:TrimMode=partial
move AnalogizerConfigurator_win_arm\AnalogizerConfigurator.exe release\AnalogizerConfigurator_win_arm32.exe
rmdir /s /q AnalogizerConfigurator_win_arm
cd release
7z	a -tzip "AnalogizerConfigurator_win_arm32.zip" "AnalogizerConfigurator_win_arm32.exe"
del AnalogizerConfigurator_win_arm32.exe
cd ..


@ECHO *************************
@ECHO ***************************
@ECHO *** Windows ARM64 Build ***
@ECHO *************************
@ECHO ***************************
dotnet publish AnalogizerConfigurator.csproj -r win-arm64 --self-contained true -c Release -o AnalogizerConfigurator_win_arm64 --consoleloggerparameters:ErrorsOnly -p:PublishTrimmed=True -p:TrimMode=partial
move AnalogizerConfigurator_win_arm64\AnalogizerConfigurator.exe release\AnalogizerConfigurator_win_arm64.exe
rmdir /s /q AnalogizerConfigurator_win_arm64
cd release
7z	a -tzip "AnalogizerConfigurator_win_arm64.zip" "AnalogizerConfigurator_win_arm64.exe"
del AnalogizerConfigurator_win_arm64.exe
cd ..


@ECHO ***********************
@ECHO *** Linux x64 Build ***
@ECHO ***********************
dotnet publish AnalogizerConfigurator.csproj -r linux-x64 --self-contained true -c Release -o AnalogizerConfigurator_linux_x64 --consoleloggerparameters:ErrorsOnly -p:PublishTrimmed=False -p:TrimMode=partial
move AnalogizerConfigurator_linux_x64\AnalogizerConfigurator release\AnalogizerConfigurator_linux_x64.bin
rmdir /s /q AnalogizerConfigurator_linux_x64
cd release
7z a -tzip "AnalogizerConfigurator_linux_x64.zip" "AnalogizerConfigurator_linux_x64.bin"
del AnalogizerConfigurator_linux_x64.bin
cd ..


@ECHO *************************
@ECHO *************************
@ECHO *** Linux ARM32 Build ***
@ECHO *************************
@ECHO *************************
dotnet publish AnalogizerConfigurator.csproj -r linux-arm --self-contained true -c Release -o AnalogizerConfigurator_linux_arm --consoleloggerparameters:ErrorsOnly -p:PublishTrimmed=False -p:TrimMode=partial
move AnalogizerConfigurator_linux_arm\AnalogizerConfigurator release\AnalogizerConfigurator_linux_arm32.bin
rmdir rmdir /s /q AnalogizerConfigurator_linux_arm
cd release
7z a -tzip "AnalogizerConfigurator_linux_arm32.zip" "AnalogizerConfigurator_linux_arm32.bin"
del AnalogizerConfigurator_linux_arm32.bin
cd ..


@ECHO *************************
@ECHO *************************
@ECHO *** Linux ARM64 Build ***
@ECHO *************************
@ECHO *************************
dotnet publish AnalogizerConfigurator.csproj -r linux-arm64 --self-contained true -c Release -o AnalogizerConfigurator_linux_arm64 --consoleloggerparameters:ErrorsOnly -p:PublishTrimmed=False -p:TrimMode=partial
move AnalogizerConfigurator_linux_arm64\AnalogizerConfigurator release\AnalogizerConfigurator_linux_arm64.bin
rmdir /s /q AnalogizerConfigurator_linux_arm64
cd release
7z a -tzip "AnalogizerConfigurator_linux_arm64.zip" "AnalogizerConfigurator_linux_arm64.bin"
del AnalogizerConfigurator_linux_arm64.bin
cd ..


@ECHO ***********************
@ECHO *** MacOS x64 Build ***
@ECHO ***********************
dotnet publish AnalogizerConfigurator.csproj -r osx-x64 --self-contained true -c Release -o AnalogizerConfigurator_osx_x64 --consoleloggerparameters:ErrorsOnly -p:PublishTrimmed=True -p:TrimMode=partial
move AnalogizerConfigurator_osx_x64\AnalogizerConfigurator release\AnalogizerConfigurator_osx_x64
rmdir /s /q AnalogizerConfigurator_osx_x64
cd release
7z a -tzip "AnalogizerConfigurator_mac_x64.zip" "AnalogizerConfigurator_osx_x64"
del AnalogizerConfigurator_osx_x64
cd ..


@ECHO *************************
@ECHO *************************
@ECHO *** MacOS ARM64 Build ***
@ECHO *************************
@ECHO *************************
dotnet publish AnalogizerConfigurator.csproj -r osx-arm64 --self-contained true -c Release -o AnalogizerConfigurator_osx_arm64 --consoleloggerparameters:ErrorsOnly -p:PublishTrimmed=True -p:TrimMode=partial
move AnalogizerConfigurator_osx_arm64\AnalogizerConfigurator release\AnalogizerConfigurator_osx_arm64
rmdir /s /q AnalogizerConfigurator_osx_arm64
cd release
7z a -tzip "AnalogizerConfigurator_mac_arm64.zip" "AnalogizerConfigurator_osx_arm64"
del AnalogizerConfigurator_osx_arm64
cd ..

