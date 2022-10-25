param(
    [Parameter()]
    [ValidateSet('CreateMigration', 'RemoveMigration')]
    [String]
    $Action = 'CreateMigration'
)

function Set-Location-To-Solution {
    Set-Location -Path "$PSScriptRoot\TransactionalOutBoxPattern"
}

function Revert-Location {
    Set-Location -Path -
}


function Restore-DotNetTool {
	Write-Host "Installing required tools..." -ForegroundColor DarkYellow
	dotnet tool restore
}

function Create-Migration {
	$MigrationName = Read-Host "Enter migration name"

    dotnet ef migrations add $MigrationName `
        -s ".\TransactionalOutBoxPattern.WebApi\TransactionalOutBoxPattern.WebApi.csproj" `
        -p ".\TransactionalOutBoxPattern.Infrastructure\TransactionalOutBoxPattern.Infrastructure.csproj"
}

function Remove-Migration {
    dotnet ef migrations remove `
        -s ".\TransactionalOutBoxPattern.WebApi\TransactionalOutBoxPattern.WebApi.csproj" `
        -p ".\TransactionalOutBoxPattern.Infrastructure\TransactionalOutBoxPattern.Infrastructure.csproj"
}

if ($Action -eq 'CreateMigration') {
    Set-Location-To-Solution
	Restore-DotNetTool
    Create-Migration
    Revert-Location
}

if ($Action -eq 'RemoveMigration') {
    Set-Location-To-Solution
	Restore-DotNetTool
    Remove-Migration
    Revert-Location
}
