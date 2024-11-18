# Используем базовый образ для Docker
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

# Создаем рабочую директорию
WORKDIR /app

# Копируем Dockerfile (1) из папки ykotika/ykotika/ykotika.WebApi в рабочую директорию
COPY ykotika/ykotika/ykotika.WebApi/Dockerfile ./Dockerfile

# Теперь используем Dockerfile (1) для сборки проекта
RUN docker build -f ./Dockerfile -t ykotika.webapi .

# Здесь можно продолжить настройку контейнера, например, его запуск
ENTRYPOINT ["dotnet", "ykotika.WebAPI.dll"]
