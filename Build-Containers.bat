@echo off

if not exist "volumes/database/seed/Discounts.json" (call Extract-DB-FakeData.bat)

echo Build Docker containers
docker-compose up --build
pause