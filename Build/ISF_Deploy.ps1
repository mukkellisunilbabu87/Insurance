param(
    [string] $isfSolutionRoot,
    [string] $deployRoot
)
Push-Location

#delete contents of the staging location
$deployContents = $deployRoot+"\*"
Remove-Item -Path $deployContents -Recurse

#First build the whole solutions

#deploy insurance.webapi
.\BuildAndCreateZipArtifact.ps1 -projectFullPath $isfSolutionRoot\Insurance.WebAPI\Insurance.WebAPI.csproj -deployRoot $deployRoot\TestInsuranceApi

#deploy insurance.webapi
.\BuildAndCreateZipArtifact.ps1 -projectFullPath $isfSolutionRoot\Insurance.MVC\Insurance.MVC.csproj -deployRoot $deployRoot\TestInsurance

#Compress-Archive -Path $deployContents -CompressionLevel Fastest -DestinationPath $deployRoot\DeployStage.zip

Pop-Location
#.\ISF_Deploy.ps1 -isfSolutionRoot D:\SreekanthProjects\ALPS\Projects\ALPSISF -deployRoot D:\DeployStage
#powershell.exe -noprofile -executionpolicy bypass -file .\ISF_Deploy.ps1 -isfSolutionRoot D:\1-Development\ALPS\ALPS_Online_Application\src\OnlineApplication -deployRoot D:\Customers\1-ALPS\DeployStage