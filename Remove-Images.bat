@echo on

docker-compose down

docker image rm --force webapi
docker image rm --force identityserver
docker image prune --force