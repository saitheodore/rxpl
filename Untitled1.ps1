[CmdletBinding()]
Param(
  [Parameter(Mandatory=$True,Position=1)]
   [string]$employeeID,
	
   [Parameter(Mandatory=$True)]
   [string]$password
)

try {
        Set-ADAccountPassword -Identity $employeeID -NewPassword (ConvertTo-SecureString $password -AsPlainText -force) -Reset
        Write-Output "$employeeID,Success"
    } catch {
        Write-Output "$employeeID,Error"
    }

Import-Module activedirectory
Import-Csv "PassChange.csv" | Foreach {
    $user = $_.employeeID
    $pw = $_.Password
    try {
        Set-ADAccountPassword -Identity $user -NewPassword (ConvertTo-SecureString $pw -AsPlainText -force) -Reset
        Write-Output "$user,Success"
    } catch {
        Write-Output "$user,Error"
    }
} | Out-File PassChange.log