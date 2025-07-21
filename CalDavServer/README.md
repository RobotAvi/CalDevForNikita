# CalDavServer

Сервер CalDAV на C# и ASP.NET Core

## Описание

Этот проект реализует сервер для хранения, управления и синхронизации календарных данных по протоколу CalDAV (RFC 4791) с поддержкой WebDAV, iCalendar (RFC 5545), ACL (RFC 3744) и OAuth2.

## Структура проекта

- Controllers/ — контроллеры API и CalDAV/WebDAV
- Models/ — модели данных (пользователь, календарь, событие, ACL)
- Services/ — бизнес-логика и обработка CalDAV/WebDAV
- Data/ — контекст базы данных (Entity Framework Core)
- DTOs/ — объекты передачи данных
- Utils/ — вспомогательные классы (работа с .ics, WebDAV)
- Tests/ — модульные и интеграционные тесты

## Быстрый старт

1. Установите .NET 7 SDK и SQL Server.
2. В корне выполните:
   ```bash
   dotnet restore
   dotnet ef database update
   dotnet run
   ```
3. Откройте Swagger UI или используйте CalDAV-клиент для тестирования.

## Основные возможности

- Аутентификация: Basic и OAuth2
- CRUD для календарей и событий
- Импорт/экспорт .ics
- ACL и делегирование
- Логирование и мониторинг
- Администрирование через API

## Документация

- [CalDAV RFC 4791](https://www.ietf.org/rfc/rfc4791.txt)
- [iCalendar RFC 5545](https://datatracker.ietf.org/doc/html/rfc5545)
- [WebDAV](https://datatracker.ietf.org/doc/html/rfc4918)