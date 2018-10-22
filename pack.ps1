if ($env:IS_RELEASE) 
{
    dotnet pack --configuration $env:buildConfiguration --output $env:BUILD_ARTIFACTSTAGINGDIRECTORY
}
else
{
    dotnet pack --version-suffix "beta.$env:BUILD_BUILDID" --configuration $env:buildConfiguration --output $env:BUILD_ARTIFACTSTAGINGDIRECTORY    
}