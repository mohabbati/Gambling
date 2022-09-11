# Introduction
A REST API implementation of a gambling game with a clean architecture!

## Requirements
* An IDE/Code Editor for example Visual Studio, VSCode or Rider
* Microsoft SQL Server as a database engine

## How to run it
1. Clone or download the repo and open it.
2. Set `Gambling.Api` as startup project.
3. Set your connection string in the `appsettings.Development.json`.
4. Run the following command in the Console Package Manager in Visual Studio (set default project to `Gambling.Data`):
	`update-database`
	**OR**
	Use .Net CLI:
	`dotnet ef database update`
	
	by this EF Core create all database object the app needs.
5. Run the startup project to see the Swagger UI.

## How to use the app
1. Sign up using the Account controller.
2. Sign in to get an access token using the Account controller.
3. Use the received access token to play the game using the Game controller. *(Add the access token to Swagger UI authorization on the top right. Or use postman.)*
