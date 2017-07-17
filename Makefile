init:
	dotnet restore

build:
	dotnet build

test:
	dotnet test ./tests/MongoIntegration.Tests/MongoIntegration.Tests.csproj

pack:
	dotnet pack ./src/MongoIntegration/MongoIntegration.csproj
