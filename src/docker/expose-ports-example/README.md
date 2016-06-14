# Expose ports

This example is displaying how one would expose the ports 80 and 443 in a docker file. Run the following command to build an image.

```
docker build -t fantasticfiasco/expose-ports-example:1.0 .
```

Then run the container using the following command.

```
docker run -d -P fantasticfiasco/expose-ports-example:1.0
```

The argument `-P` is telling docker to automatically map the exposed container ports to ports on the host (in the range of 49153-65535). To verify the opened ports on the host, please run the following command.

```
docker ps
```