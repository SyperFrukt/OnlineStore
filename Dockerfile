FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Копируем ТОЛЬКО файлы проектов для эффективного кеширования
COPY OnlineStore.Domain/OnlineStore.Domain.csproj OnlineStore.Domain/
COPY OnlineStore.Infrastructure/OnlineStore.Infrastructure.csproj OnlineStore.Infrastructure/
COPY OnlineStore.Services/OnlineStore.Services.csproj OnlineStore.Services/
COPY OnlineStore.Web/OnlineStore.Web.csproj OnlineStore.Web/

# 2. Восстанавливаем NuGet-пакеты (строго для веб-проекта, он подтянет все зависимые)
RUN dotnet restore OnlineStore.Web/OnlineStore.Web.csproj

# 3. Копируем весь исходный код
COPY . .

# 4. Публикуем (--no-restore ускоряет сборку, т.к. пакеты уже скачаны)
WORKDIR /src/OnlineStore.Web
RUN dotnet publish -c Release -o /app/publish --no-restore

# Этап 2: Финальный образ (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
RUN mkdir -p /app/data
COPY --from=build /app/publish .

# Настройки окружения
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "OnlineStore.Web.dll"]
