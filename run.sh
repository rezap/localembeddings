docker image rm le
docker image prune -f --all
docker container prune -f
docker build -t le --platform linux/amd64 .
docker run -it --rm -p 8080:8080 le