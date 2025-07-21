

# Техническое задание: Сервер CalDAV на C# и ASP.NET

## 1. Введение

**Цель проекта:** создать сервер для хранения, управления и синхронизации календарных данных по протоколу CalDAV, реализованный на языке C# с использованием ASP.NET.  
**Клиенты:** интерфейс совместим с приложениями и сервисами, поддерживающими CalDAV (например, мобильные календари, почтовые клиенты, web-платформы).

## 2. Требования к функционалу

### 2.1 Поддерживаемые протоколы и стандарты

- Реализация CalDAV согласно RFC 4791, а также базовая поддержка WebDAV и расширения для планирования (RFC 6638)[1][2].
- Формат обмена — iCalendar (RFC 5545).
- Использование стандартных HTTP-методов для обработки запросов: `GET`, `PUT`, `DELETE`, `PROPFIND`, `REPORT` и др.
- Поддержка календарных коллекций, ресурсов событий и задач.

### 2.2 Аутентификация и безопасность

- Авторизация пользователей через базовую аутентификацию или OAuth 2.0.
- Хранение паролей с использованием современных алгоритмов хеширования.
- Поддержка HTTPS для всех соединений.
- Гибкая система прав доступа и ACL для календари и событий (RFC 3744)[3][2].

### 2.3 Работа с календарями

- Создание/удаление/обновление календарей пользователями.
- Импорт и экспорт событий в формате iCalendar.
- Поиск событий по параметрам (дата, название, участники и др.).
- Управление правами доступа: совместное использование календаря, делегирование.

### 2.4 События и задачи

- Поддержка создания, редактирования, удаления событий и задач.
- Работа с повторяющимися событиями и напоминаниями.
- Поддержка статусов событий (подтверждение, отклонение, ожидание).
- Формирование отчетов о занятом/свободном времени (free-busy).

### 2.5 Совместимость

- Корректная работа с популярными клиентами (Apple Calendar, Thunderbird, Outlook и др.)[4][5].
- Взаимодействие с мобильными и десктопными приложениями через стандартные CalDAV-клиенты.
- API для извлечения и изменения календарных данных сторонними сервисами.

### 2.6 Персистентность и производительность

- Хранение данных в SQL-базе (например, MSSQL, поддержка ORM).
- Работа под высокой нагрузкой; масштабируемая архитектура.
- Логирование действий и ошибок для мониторинга и диагностики[4].

## 3. Требования к архитектуре и реализации

### 3.1 Технологии и стек

- Язык разработки: C# (версия 8.0 и выше).
- Платформа: ASP.NET Core (версия 5.0 и выше).
- База данных: Microsoft SQL Server или совместимая с EF Core.
- Версионирование API при необходимости.

### 3.2 REST API и обработка запросов

- Обработка WebDAV и CalDAV-запросов согласно спецификациям.
- Корректная реакция на запросы типов OPTIONS, PROPFIND, REPORT, PUT, DELETE, MKCALENDAR и др.
- Поддержка многосессийной обработки (async/await, параллелизм).

### 3.3 Безопасность

- Реализация HTTPS с возможностью управления сертификатами.
- Защита от XSS, CSRF, SQL-injection.

### 3.4 Тестирование

- Модульные тесты для ключевой логики.
- Интеграционное тестирование взаимодействия протоколов.
- Юзабилити-тестирование совместимости с реальными клиентами.

### 3.5 Документация

- Архитектурная документация.
- Описание публичного API.
- Руководство для установки и эксплуатации.

## 4. Администрирование и поддержка

- Веб-интерфейс/консоль администрирования пользователей, календарей и прав доступа.
- Возможность резервного копирования и восстановления календарных данных.

## 5. Критерии готовности (Definition of Done)

- Корректная работа с каноническими CalDAV-клиентами.
- Соответствие RFC-стандартам.
- Покрытие основных сценариев юнит- и интеграционными тестами.
- Документация предоставлена и легко доступна.

**Примечание:** Возможна интеграция готовых библиотек для работы с iCalendar (.ics), например iCal.NET, или реализация собственного парсера данных[6][7].

**Источники:**  
[1], [2], [4], [3], [5], [6], [7]

Цитаты:
[1] CalDAV https://www.ietf.org/rfc/rfc4791.txt
[2] CalDAV https://ru.wikipedia.org/wiki/CalDAV
[3] CalDAV https://en.wikipedia.org/wiki/CalDAV
[4] CalDAV Server with SQL Back-end Example https://www.webdavsystem.com/server/server_examples/caldav_sql/
[5] CalDAV and CardDAV 101 https://www.systutorials.com/caldav-and-carddav-101/
[6] Creating iCal ics Files in C# ASP.NET Core - Dark Code Space https://stayrony.github.io/Create-iCal-ics-Files-in-net-Core/
[7] Interact with CalDav Server from asp.net MVC - Stack Overflow https://stackoverflow.com/questions/43161259/interact-with-caldav-server-from-asp-net-mvc
[8] CalDAV API Developer's Guide | Google Calendar https://developers.google.com/workspace/calendar/caldav/v2/guide
[9] RFC 7809 - Calendaring Extensions to WebDAV (CalDAV) https://datatracker.ietf.org/doc/html/rfc7809
[10] RFC 6764: Locating Services for Calendaring Extensions ... https://www.rfc-editor.org/rfc/rfc6764.html
[11] Creating CalDAV Server https://www.webdavsystem.com/server/creating_caldav_carddav/creating_caldav_server/
[12] caldav · GitHub Topics https://github.com/topics/caldav?l=c%23&o=asc&s=forks
[13] CalDAV - What's behind the network protocol? - IONOS https://www.ionos.com/digitalguide/e-mail/technical-matters/caldav/
[14] CalDAV and CardDAV : Technical Documentation https://documentation.open-xchange.com/7.10.5/middleware/miscellaneous/caldav_carddav.html
[15] CalDAV - Introduction - CalConnect https://devguide.calconnect.org/CalDAV/introduction/
[16] CalDAV — Cyrus IMAP 3.2.12 documentation https://www.cyrusimap.org/3.2/imap/download/installation/http/caldav.html
[17] Building a CalDAV client https://sabre.io/dav/building-a-caldav-client/
[18] Calendar Server Example, C# - IT Hit WebDAV Server Engine https://www.webdavsystem.com/server/server_examples/calendar_server_csharp/
[19] Calendar Protocols https://www.emclient.com/documentation/technologies-advanced/protocols/calendar-protocols
[20] CalDAV Implementation Details | Axigen Documentation https://www.axigen.com/documentation/caldav-implementation-details-p47120760
