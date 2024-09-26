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