pushd ./dotnet

dotnet restore

dotnet build --configuration $env:buildConfiguration

popd