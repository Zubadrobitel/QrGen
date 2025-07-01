# QrGen
Приложение для генерации QR-кодов
Это небольшое приложение web-api, созданное для генерации QR-кодов с информацией о заезде, выезде, пароле и количестве гостей для отеля

## Стек технологий:
- EntityFrameworkCore  
- PostgreSQL  
- ASP.NET 8  
- Swagger

# Инструкция по запуску
**Необходим Git Bash либо любое другое приложение, или инструментарий для проведения миграции базы данны, инструкция ниже используется при работе с Git Bash**
1. В проекте есть файл appsettings.example.json, это пример конфига и настройки для запука:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ApplicationDataBaseContext": "Host=localhost;Port=5432;Database=<DB_NAME>;Username=<USERNAME>;Password=<PASSWORD>"
  }
}
```
Его нужно скопировать, переиминовать в appsettings.json и подставить параметры для подключения к базе данных:  
- Database=<DB_NAME>(название базы данных)
- Username=<USERNAME>(имя пользователя базы данных)
- Password=<PASSWORD>(пароль)  
2. Установка инструментов EF Core:
```bash
dotnet tool install --global dotnet-ef
```
3. Приминение миграции(в коробке идёт миграция инициализации базы данных):
```bash
dotnet ef database update --project src/QrGen.DataBase --startup-project src/QrGen.API
```
4. Запуск проекта:
```
dotnet run --project src/QrGen.API
```
Либо через графический интерфейс Visual Studio нажав на кнопку старта отладки, главное выполнить первые 3 пункта  
# Ключевые возможности
**Хранение информации о QR-кодах**  

**QrCode:**  
- Id(Уникальный идентификатор)  
- CreatedAt(Дата создания)  
- UpdatedAt(Дата обновления)  
- InfoId(Внешний ключ, используется для каскадного удаления данных)

**QrInfo:**  
- Id(Уникальный идентификатор, совпадает с InfoId в QrInfo)  
- Password(Пароль)  
- Start(Дата заезда)  
- End(Дата отъезда)  
- GuestCount(Кол-во гостей, минимум 1)

**Функционал:**  
- Создание QR-кода
- Получение всех QR-кодов
- Удаление QR-кода
- Получение одного QR-кода  

## API запросы
**Генерация QR-кодов на основе данных:**  
- id(совпадает с  InfoId)
- password(Пароля)  
- start(Даты заезда)  
- end(Даты выезда)  
- guestCount(Количество гостей)

Запрос:
```
POST /QrCodes/create
{
  "password": "secure_password_123",
  "start": "2025-06-30T00:00:00Z",
  "end": "2025-06-30T23:59:59Z",
  "guestCount": 5
}
```
Ответ будет содержать Id созданного Qr-кода:
```
3fa85f64-5717-4562-b3fc-2c963f66afa6
```
**Получение QR-кода по id с информацией:**  
- id(уникальный идентификатор)  
- createdAt(Дата создания)  
- qrCodeAsBase64(Изображение в формате base64)

Запрос:
```
GET /qr-code/{id}
```
Ответ:
```
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "createdAt": "2025-06-30T01:32:12.896Z",
  "qrCodeAsBase64": "string"
}
```
**Получение всех созданных QR-кодов с информацией:**  
- id(уникальный идентификатор)  
- createdAt(Дата создания)  
- qrCodeAsBase64(Изображение в формате base64)  

Запрос:
```
GET /QrCodes/qr-codes/
```
Ответ:
```
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "createdAt": "2025-06-30T01:39:07.195Z",
    "qrCodeAsBase64": "string"
  }
]
```
**Удаление QR-кода и всех связанных с ним данных по id**
- id(уникальный идентификатор)

Запрос:
```
POST /QrCodes/delete/{id}
```
Ответ:
```
200	OK
```
## Swagger спецификация:
https://localhost:7176/swagger/
