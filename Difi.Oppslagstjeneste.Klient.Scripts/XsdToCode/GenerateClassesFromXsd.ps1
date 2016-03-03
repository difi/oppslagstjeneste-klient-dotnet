# RUNNING THE FIRST TIME
#   Set-ExecutionPolicy Bypass

Write-Host "- If on Vm-Ware host and script on Z: drive:"
Write-Host "   net use Z: `"\\vmware-host\Shared Folders`""

function GenerateCode($XsdPath, $OutDir)
{
    Write-Host("XSD: $Xsd")
    Write-Host("OUT: $OutDir")
    
    $CurrentDirectory = (Get-Item -Path ".\" -Verbose).FullName
	Set-Location "C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6 Tools\"
    Invoke-Expression ".\Xsd.exe $XsdPath /classes /out:$OutDir"
    Set-Location $CurrentDirectory
}

function Get-ScriptDirectory
{
  $Invocation = (Get-Variable MyInvocation -Scope 1).Value
  Split-Path $Invocation.MyCommand.Path
}

$CurrentScriptDir = Get-ScriptDirectory

$Xsd1 = "\Xsd\oppslagstjeneste-ws-16-02.xsd"
$XsdRef1 = "\Xsd\oppslagstjeneste-metadata-16-02.xsd"
$XsdPath = "$CurrentScriptDir$Xsd1 $CurrentScriptDir$XsdRef1"

$XsdToCodeDir = (Get-Item $CurrentScriptDir)
$CodeDir =  "$XsdToCodeDir\Code"

GenerateCode $XsdPath "$CodeDir"