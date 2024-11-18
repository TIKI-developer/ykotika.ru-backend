# Используем базовый образ для ASP.NET Core на Linux
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Используем образ SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

# Копируем только файл .csproj и восстанавливаем зависимости
WORKDIR /src
COPY ykotika.Application/ykotika.Application.csproj app/ykotika.Application/
COPY ykotika.Domain/ykotika.Domain.csproj app/ykotika.Domain/
COPY ykotika.Security/ykotika.Security.csproj app/ykotika.Security/
COPY ykotika.Persistence/ykotika.Persistence.csproj app/ykotika.Persistence/
COPY ykotika.WebAPI/ykotika.WebAPI.csproj app/ykotika.WebAPI/
RUN dotnet restore "ykotika.WebAPI/ykotika.WebAPI.csproj"

# Копируем оставшиеся файлы проекта и собираем приложение
COPY /src .
WORKDIR "/src/ykotika.WebAPI"
RUN dotnet build "ykotika.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Публикуем приложение в папку /app/publish
FROM build AS publish
RUN dotnet publish "ykotika.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный этап: использование базового образа и копирование опубликованных файлов
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ykotika.WebAPI.dll"]
