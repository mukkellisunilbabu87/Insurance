param(
    [String]$projectFullPath = "",
    [String]$deployRoot = ""
)
$ErrorActionPreference = "Stop"
function Build-ProjectAndDeployLocal($projectDirectory, $fileName, $deployPath) {
    Push-Location
    Set-Location $projectDirectory
    $msbuildPath = Get-MsBuild
    & $msbuildPath $fileName /p:configuration="Release" /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:PrecompileBeforePublish=true /p:publishUrl=$deployPath
    Pop-Location
}
function New-ZipFile($zipFileName, $zipDirectory, $directoryContentToZip) {
    $directoryContentWithWildcard = ("{0}*" -f $directoryContentToZip)
    $destinationPath = [System.IO.Path]::Combine($zipDirectory, $zipFileName)
    Write-Host ("Writing zip file to {0}" -f $destinationPath)
    Compress-Archive -Path $directoryContentWithWildcard -DestinationPath $destinationPath
    Write-Host ("Zip file written to {0}" -f $destinationPath)
	$global:zipFileLocation = $destinationPath
	Write-Host ("Deploy state has been set to {0}" -f $global:zipFileLocation)
	
}

function Get-MsBuild() {
    $path = & "C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -products * -requires Microsoft.Component.MSBuild -property installationPath
    if ($path) {
        $tool = join-path $path 'MSBuild\Current\Bin\MSBuild.exe'
        if (-not (test-path $tool)) {
            $tool = join-path $path 'MSBuild\15.0\Bin\MSBuild.exe'
            if (-not (test-path $tool)) {
                throw 'Failed to find MSBuild'
            }
        }
        return $tool
    }
}


$deployPath = [System.IO.Path]::Combine($deployRoot, "WebSiteContent\")
$zipDirectory = [System.IO.Path]::Combine($deployRoot, "WebSiteZip\")

if (-not (Test-Path $projectFullPath)) {
    Write-Host ("Error file {0} does not exist" -f $projectFullPath) -ForegroundColor Red
}
if(Test-Path $zipDirectory){
    [System.IO.Directory]::Delete($zipDirectory, $true)
}
if(Test-Path $deployPath){
    [System.IO.Directory]::Delete($deployPath, $true)
}
if (-not (Test-Path $zipDirectory)) {
    New-Item $zipDirectory -ItemType Directory
}

if (-not (Test-Path $deployPath)) {
    New-Item $deployPath -ItemType Directory
}

$projectDirectory = [System.IO.Directory]::GetParent($projectFullPath).FullName
$fileName = [System.IO.Path]::GetFileName($projectFullPath)
$zipFileName = ("{0}.zip" -f [System.IO.Path]::GetFileNameWithoutExtension($fileName))

Write-Host ("project directory is {0}" -f $projectDirectory)
Write-Host ("File host is {0}" -f $fileName)
Write-Host ("Zip file is {0}" -f $zipFileName)

Build-ProjectAndDeployLocal -projectDirectory $projectDirectory -fileName $fileName -deployPath $deployPath
New-ZipFile -zipFileName $zipFileName -zipDirectory $zipDirectory -directoryContentToZip $deployPath



