# WindowsServiceNetCore
Creating a Windows Service in Net Core 2.1 using HTTP CLIENT and Integration
# Packages to install
Install-Package Microsoft.Windows.Compatibility
# Deploy service	
dotnet publish -r win-x64 -c Release  <br>
or
dotnet publish -r win-x86 -c Release
# Create Service
sc create TestService BinPath=C:\full\path\to\publish\dir\WindowsServiceExample.exe
