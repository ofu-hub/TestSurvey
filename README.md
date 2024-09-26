# TestSurvey

Это приложение для сбора опросов, которое позволяет пользователям создавать и анализировать опросы.

## Содержание

- [Описание](#описание)
- [Требования](#требования)
- [Установка](#установка)
- [Использование](#использование)
- [Docker](#docker)

## Описание

Данный проект предназначен для управления опросами. Пользователи могут создавать опросы, заполнять их и просматривать результаты. Приложение реализовано с использованием ASP.NET Core и базы данных PostgreSQL.

## Требования

- [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [PostgreSQL](https://www.postgresql.org/) (если не используется Docker)
- [JetBrains Rider](https://www.jetbrains.com/rider/)

## Установка

### Локальная установка

1. Склонируйте репозиторий:
```bash
   git clone https://github.com/ofu-hub/TestSurvey.git
   cd TestSurvey
```
2. Установите зависимости:

```bash
    dotnet restore
```

3. Настройте базу данных:

- Создайте базу данных в PostgreSQL.
- Откройте файл appsettings.json и добавьте необходимые переменные окружения, например:

```json
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=survey_db;Username=yourusername;Password=yourpassword"
  },
```

4. Примените миграции:

```bash
    dotnet ef database update
```

## Использование

Запустите приложение с помощью команды:

```bash
    dotnet run
```

После этого приложение будет доступно по адресу http://localhost:{ваш порт}.

## Docker
### Запуск с помощью Docker

Для удобства вы можете запустить проект с помощью Docker. Для этого выполните следующие шаги:
- Убедитесь, что у вас установлен Docker.
- Создайте файл docker-compose.yml в корне проекта и добавьте следующий код:

```yaml
        version: '3.8'

        services:
        survey-api:
            container_name: survey-api
            build:
            context: .
            dockerfile: Dockerfile
            hostname: survey-api
            networks:
            - survey-net
            ports:
            - "7142:80"
            depends_on:
            - survey_db
            restart: always

        survey_db:
            container_name: postgres
            image: postgres:14
            hostname: postgres
            networks:
            - survey-net
            environment:
            POSTGRES_USER: ${PG_USER}
            POSTGRES_PASSWORD: ${PG_PASSWORD}
            POSTGRES_DB: survey_db
            ports:
            - "5433:5432"
            volumes:
            - pg-data:/var/lib/postgresql/data
            restart: always
        
        pgadmin:
            image: dpage/pgadmin4
            container_name: pgadmin4
            hostname: pgadmin4
            networks:
            - survey-net
            ports:
            - "5050:5433"
            volumes:
            - pgadmin:/var/lib/pgadmin
            environment:
            PGADMIN_DEFAULT_EMAIL: ${PGADMIN_DEFAULT_EMAIL}
            PGADMIN_DEFAULT_PASSWORD: ${PGADMIN_DEFAULT_PASSWORD}
            PGADMIN_LISTEN_PORT: 5433
            depends_on:
            - survey_db
            restart: always

        networks:
        survey-net:
            driver: bridge

        volumes:
        pg-data: {}
        pgadmin: {}
```

- Создайте файл Dockerfile в корне проекта и добавьте следующий код:
    
```dockerfile
    # Базовый образ для сборки
    FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
    WORKDIR /app
    EXPOSE 80
    EXPOSE 443

    FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
    WORKDIR /src

    # Копируем файлы проекта
    COPY ["Survey.Api/Survey.Api.csproj", "Survey.Api/"]
    COPY ["Survey.Application/Survey.Application.csproj", "Survey.Application/"]
    COPY ["Survey.Domain/Survey.Domain.csproj", "Survey.Domain/"]
    COPY ["Survey.Infrastructure/Survey.Infrastructure.csproj", "Survey.Infrastructure/"]

    # Восстанавливаем зависимости
    RUN dotnet restore Survey.Api/Survey.Api.csproj

    # Копируем все остальные файлы
    COPY . .

    # Собираем приложение
    WORKDIR /src/Survey.Api
    RUN dotnet build "Survey.Api.csproj" -c Release -o /app/build

    # Публикуем приложение
    FROM build AS publish
    RUN dotnet publish "Survey.Api.csproj" -c Release -o /app/publish

    # Создаем финальный образ
    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .

    # Указываем точку входа
    ENTRYPOINT ["dotnet", "Survey.Api.dll"]
```

- Запустите docker-compose
```bash
docker-compose up --build
```