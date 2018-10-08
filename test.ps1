pushd ./dotnet

$hasErrors = $false
$lastDirectory=""
$directories = (Get-ChildItem -Path test -Directory);

foreach ($directory in $directories)
{
    pushd $directory.FullName
    
    
    if ($directory.FullName -like "*Tests*") {
        dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --configuration $env:buildConfiguration --logger trx
    }
    
    if ($LastExitCode -ne 0) {
        $hasErrors = $true
    }
    
    popd
    $lastDirectory = $directory.FullName
}
popd


pushd $lastDirectory

dotnet reportgenerator "-reports:../**/coverage.cobertura.xml" "-targetdir:../../CoverageReports" "-reporttypes:htmlInline"

popd

if ($hasErrors) {
    exit 1
}