# .NET Core Hello World on Docker

This sample is the result of following the article [Running .NET Core on Docker](https://medium.com/trafi-tech-beat/running-net-core-on-docker-c438889eb5a#.ayebzdocc).

## Usage

Restore NuGet packages
```
dotnet restore
```

Run application
```
dotnet run
```

Publish application
```
dotnet publish
```

Build Docker container
```
docker build -t hello-world .
```

Run Docker container
```
docker run -it -p 80:80 hello-world
```