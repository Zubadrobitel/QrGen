@echo off
chcp 65001 > nul

echo [1/5] Чистим старые контейнеры...
docker-compose down -v
echo Готово!
echo.

echo [2/5] Чистим мусор...
docker system prune -f
echo Готово!
echo.

echo [3/5] Сборка контейнеов, настройка аркестрации...
docker-compose --env-file .env.production up -d --build
echo Готово!
echo.

echo [4/5] Проверяем состояние...
timeout /t 5 /nobreak > nul
curl -s http://localhost:5431/health | find "Healthy"
if %errorlevel% equ 0 (
    echo API здоров!
) else (
    echo API не отвечает!
)
echo.

echo [5/5] Статус контейнеров:
echo.
docker ps --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}"
echo.