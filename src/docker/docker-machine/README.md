# Docker Machine

Docker Machine is a tool that simplifies setting up a Docker host on a machine. As an example, after Docker Toolbox is installed, a host with the name _default_ is installed on the computer.

To see all installed hosts on the computer, run the following command.

```
docker-machine ls
```

## Drivers

When running on a local computer, the [VirtualBox](https://www.virtualbox.org) driver is used. But when you wish to run Docker Machine on the cloud you have to install a driver that is compatible with your cloud provider. 

## Create a host

To create a new host on Windows or Mac, run the following command.

```
docker-machine create --driver virtualbox testhost
```