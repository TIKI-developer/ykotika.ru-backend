# Используем официальный образ .NET SDK для сборки приложения (с .NET 8)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Устанавливаем рабочую директорию для сборки
#WORKDIR /src

# Копируем решение и все проекты в контейнер
COPY src/ykotika.sln ./
COPY src/ykotika.Domain ./src/ykotika.Domain
COPY src/ykotika.Application ./src/ykotika.Application
COPY src/ykotika.Security ./src/ykotika.Security
COPY src/ykotika.Persistence ./src/ykotika.Persistence
COPY src/ykotika.WebAPI ./src/ykotika.WebAPI

# Восстанавливаем зависимости всех проектов
RUN dotnet restore ykotika.sln

# Сборка проекта
RUN dotnet publish src/ykotika.WebAPI/ykotika.WebAPI.csproj -c Release -o /app

# Используем официальный образ .NET Runtime для запуска приложения (с .NET 8)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Устанавливаем рабочую директорию для исполнимого файла
WORKDIR /app

# Копируем собранное приложение из стадии сборки
COPY --from=build /app .

# Открываем порт для приложения
EXPOSE 80

# Указываем команду для запуска веб-приложения
ENTRYPOINT ["dotnet", "ykotika.WebAPI.dll"]
