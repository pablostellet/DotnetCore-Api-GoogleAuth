api:
	dotnet run -p src/API/base-template.csproj

clean:
	dotnet clean src/API/base-template.csproj

angular: 
	npm start --prefix src/Client/Angular
