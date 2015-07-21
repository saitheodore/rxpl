[CmdletBinding()]
Param(
  [Parameter(Mandatory=$True,Position=1)]
   [string]$employeeID,
	
   [Parameter(Mandatory=$True)]
   [string]$password
)

Set-ADAccountPassword -Identity $employeeID -NewPassword (ConvertTo-SecureString $password -AsPlainText -force) -Reset
Write-Output "$employeeID,Success"