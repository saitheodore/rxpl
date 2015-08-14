[CmdletBinding()]
Param(
  [Parameter(Mandatory=$True,Position=1)]
   [string]$employeeID,
	
   [Parameter(Mandatory=$True)]
   [string]$password
)

Set-Content -Path "D:\Sai\Projects\RXPL.AD.Service\test.txt" -Value ($employeeID + $password)