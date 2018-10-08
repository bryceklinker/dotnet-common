pushd ./dotnet

dotnet pack --version-suffix "beta.$env:BUILD_BUILDID" --configuration $env:buildConfiguration --output $env:BUILD_ARTIFACTSTAGINGDIRECTORY

popd