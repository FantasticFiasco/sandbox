# Example of a comment
FROM ubuntu:14.04
# To prevent multiple commits to the image one can chain commands like this
RUN apt-get update && apt-get install -y \
	curl \
	vim
# Define the entry point for the image. Entry points, in contrast to CMD, cannot be overridden when using `docker run`
ENTRYPOINT ["ping"]
# Define the default arguments passed to the entry point
CMD ["127.0.0.1", "-c", "10"]