FROM microsoft/dotnet:latest
COPY bin/Debug/netcoreapp1.0/publish/ /root/
EXPOSE 5000/tcp
ENTRYPOINT dotnet /root/docker-hello-world.dll