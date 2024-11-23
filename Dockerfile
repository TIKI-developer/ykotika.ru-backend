# Используем официальный образ .NET SDK для сборки приложения (с .NET 8)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Устанавливаем рабочую директорию для сборки
WORKDIR /src

# Копируем всю директорию с исходным кодом в контейнер
COPY src/ ./

# Устанавливаем переменную окружения для Development
ENV ASPNETCORE_ENVIRONMENT Development

# Очищаем проект перед сборкой
RUN dotnet clean Ykotika.sln

# Сборка проекта
RUN dotnet build Ykotika.sln -c Debug

# Публикуем проект
RUN dotnet publish Ykotika.WebAPI/Ykotika.WebAPI.csproj -c Debug -o /app

# Используем официальный образ .NET Runtime для запуска приложения (с .NET 8)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Устанавливаем рабочую директорию для исполнимого файла
WORKDIR /app

# Копируем собранное приложение из стадии сборки
COPY --from=build /app ./

# Устанавливаем переменную окружения для Development в контейнере
ENV ASPNETCORE_ENVIRONMENT Development

# Открываем порт для приложения
EXPOSE 8080
EXPOSE 8081

# Указываем команду для запуска веб-приложения
ENTRYPOINT ["dotnet", "Ykotika.WebAPI.dll"]
