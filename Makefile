api:
	dotnet run -p src/DotnetAPI/base-template.csproj

clean:
	dotnet clean src/DotnetAPI/base-template.csproj

angular: 
	npm start --prefix src/Client/Angular
