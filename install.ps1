dotnet tool update -g powershell
dotnet tool update -g gitversion.tool
git submodule update --init

cd .config
git checkout master
cd ..

pwsh -File .config/scripts/windows/base.ps1
## Any required environments
pwsh -File .config/scripts/windows/dotnet.ps1
Read-Host
