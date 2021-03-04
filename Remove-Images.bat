@echo on

docker-compose down

docker image rm --force webapi
docker image rm --force identityserver
docker image rm --force mailsender
docker image prune --force