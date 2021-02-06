build:
	dotnet build
release:
	dotnet build -c Release
clean:
	dotnet clean
restore:
	dotnet restore
test:
	dotnet test
run:
	dotnet run -p HowToPool2/HowToPool.csproj

