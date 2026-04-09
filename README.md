&#x20;OnlineStore — Курсовой проект



> Веб-приложение онлайн-магазина на .NET 8 + Blazor Server + EF Core + Docker

> Дисциплина: Кроссплатформенная среда исполнения ПО

> Тема: Разработка сайта онлайн-магазина







О проекте

Практическое веб-приложение для каталогизации и заказа товаров. Реализует полный цикл: от витрины до оформления заказа с валидацией данных. Данные хранятся в SQLite, управляются через EF Core Code First. Приложение полностью контейнеризировано и готово к развёртыванию.







Архитектура и технологический стек



Компонентная схема (C4 Level 2)

┌─────────────────┐       ┌──────────────────────────────────┐

│   Клиент (Browser)     │          |   ASP.NET Core Blazor Server     │

│   (HTTPS/SignalR)      │◄────► |   • Razor Components \& Routing   │

└─────────────────┘          |   • Forms + FluentValidation     │

&#x20;                                   │   • DI (IServiceCollection)      │

&#x20;                                └──────────────────────────────────┘

&#x20;                                          │

&#x20;                                          ▼

&#x20;                         ┌──────────────────────────────────┐

&#x20;                         │   Business \& Data Layer          │

&#x20;                         │   • GenericRepository<T>         │

&#x20;                         │   • Services (IProductService)   │

&#x20;                         │   • EF Core DbContext + Fluent API│

&#x20;                         └──────────────────────────────────┘

&#x20;                                          │

&#x20;                                          ▼

&#x20;                         ┌──────────────────────────────────┐

&#x20;                         │   Database (SQLite)              │

&#x20;                         │   • Categories (1) → (N) Products│

&#x20;                         │   • Orders                       │

&#x20;                         └──────────────────────────────────┘





Технологии

| Компонент | Стек | Версия |

|-----------|------|--------|

| Платформа | .NET / ASP.NET Core | 8.0+ |

| Веб-интерфейс | Blazor Server + Razor | 8.0 |

| ORM / БД | Entity Framework Core + SQLite | 8.0.10 / 3.x |

| Валидация | FluentValidation | 11.9.2 |

| Контейнеризация | Docker + Docker Compose | Latest |

| Тестирование | xUnit (опционально) | 2.6+ |

| Качество кода | StyleCop.Analyzers, .editorconfig, XML-комментарии | - |







Структура проекта



OnlineStore/

├── OnlineStore.sln                 # Решение

├── Dockerfile                      # Multi-stage сборка

├──  docker-compose.yml              # Оркестрация (app + SQLite volume)

├──  .dockerignore                   # Исключения для Docker

├──  .editorconfig                   # Настройки стиля кода

├── README.md                       # Эта документация

│

├── OnlineStore.Domain/             # Модели сущностей

│   └── Entities/                   # Category, Product, Order

│

├──  OnlineStore.Infrastructure/     # Доступ к данным

│   ├── Data/                       # AppDbContext, Fluent API, DbSeeder

│   └── Repositories/               # GenericRepository<T>

│

├── OnlineStore.Services/           # Бизнес-логика

│   ├── Interfaces/                 # IProductService

│   ├── ProductService.cs

│   └── OrderValidator.cs              # FluentValidation правила

│

├──  OnlineStore.Web/                # Blazor Server приложение

│   ├── Program.cs                  # DI регистрация, точка входа

│   ├── appsettings.json            # Конфигурация (нет hardcoded)

│   ├── Pages/                      # Catalog.razor, Checkout.razor

│   └──  Components/                 # Layout, Routing, Reconnect

│

└── OnlineStore.Tests/              # Unit-тесты (xUnit)









Быстрый старт (Локально)



Предварительные требования:\*\* .NET 8 SDK, PowerShell/Terminal.



powershell

\# 1. Клонирование и переход в директорию

git clone <url-репозитория>

cd OnlineStore



\# 2. Восстановление зависимостей

dotnet restore



\# 3. Применение миграций БД (Code First)

cd OnlineStore.Web

dotnet ef database update



\# 4. Запуск приложения

dotnet run



\# 5. Открытие в браузере

\# → https://localhost:7XXX (порт отображается в консоли)



Запуск в Docker



Предварительные требования:\*\* Docker Desktop, WSL2 (Windows) или нативный Docker.



powershell

\# 1. Сборка и запуск контейнеров

docker-compose up --build -d



\# 2. Проверка статуса

docker-compose ps

\# Ожидаемый вывод: onlinestore-app  Up  0.0.0.0:8080->8080/tcp



\# 3. Открытие приложения

\# → http://localhost:8080



\# 4. Управление данными (Volume)

docker-compose down          # Остановка (данные БД сохраняются)

docker-compose down -v       # Полное удаление включая БД







