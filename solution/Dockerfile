# 1. Билд проекта
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файл решения и проекта
COPY solution.sln .
COPY project/project.csproj ./project/

# Восстанавливаем зависимости
RUN dotnet restore

# Копируем остальные исходные файлы
COPY project/ ./project/

# Собираем проект в Release
RUN dotnet publish project/project.csproj -c Release -o /app/publish

# 2. Финальный контейнер
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Запуск
ENTRYPOINT ["dotnet", "project.dll"]
