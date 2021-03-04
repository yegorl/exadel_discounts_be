@echo off

if not exist "volumes/database/seed/Discounts.json" (call Extract-DB-FakeData.bat)

call Stop-Containers.bat
call Remove-Images.bat

echo Rebuild Docker containers
docker-compose up --build

pause