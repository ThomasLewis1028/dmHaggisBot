# Entity Framework Links
### Getting Started
https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
### Updating Schema
https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

# Unit Testing links
https://learn.microsoft.com/en-us/visualstudio/test/using-microsoft-visualstudio-testtools-unittesting-members-in-unit-tests?view=vs-2022

# Repository Factory Method
https://codingblast.com/entity-framework-core-generic-repository/

# Setting up/Updating the Database
1. Change current directory to the `SWNUniverseGenerator` project folder.
2. Run `dotnet ef migrations add <migrationName>` where `<migrationName>` is the name of the current migration.
3. Run `dotnet ef database update`.

# Database Location Development (appsettings.Development.json)
The local database file, on Linux, should be located in `~/.local/share/universe.db`
{
"ConnectionStrings": {
"DefaultConnection": "DataSource=/home/<USER>/.local/share/universe.db;"
},
"DetailedErrors": true,
"Logging": {
"LogLevel": {
"Default": "Information",
"Microsoft": "Warning",
"Microsoft.Hosting.Lifetime": "Information"
}
}
}

# Database Location (appsettings.json)
The server database file, on Linux, should be located in `/usr/db/universe.db`
{
"ConnectionStrings": {
"DefaultConnection": "DataSource=/usr/db/universe.db;"
},
"DetailedErrors": true,
"Logging": {
"LogLevel": {
"Default": "Information",
"Microsoft": "Warning",
"Microsoft.Hosting.Lifetime": "Information"
}
}
}