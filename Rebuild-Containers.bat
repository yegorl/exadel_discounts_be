@echo off

call Remove-Images.bat

echo Rebuild Docker containers
docker-compose up --build