[CmdletBinding()]
Param(
  [Parameter(Mandatory=$True,Position=1)]
   [string]$employeeID,
	
   [Parameter(Mandatory=$True)]
   [string]$password
)

$filter = 'SamAccountName -like "*' + $employeeID + '*"'
$user = Get-ADUser -Filter $filter

if($user -eq $null -or $user.SamAccountName -eq $null -or $user.SamAccountName -eq "")
{
    throw "User with id $employeeID could not be found"
}
else
{
    Set-ADAccountPassword -Identity $user.SamAccountName -NewPassword (ConvertTo-SecureString $password -AsPlainText -force) -Reset
}

#Write-Output $user.SamAccountName