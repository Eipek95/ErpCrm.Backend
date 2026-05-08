# ERP CRM Backend System

<p align="center">
  <a href="#-türkçe-dokümantasyon">🇹🇷 Türkçe</a> |
  <a href="#-english-documentation">🇬🇧 English</a>
</p>

---

# 🇹🇷 Türkçe Dokümantasyon

## Proje Hakkında

Bu proje, modern .NET teknolojileri kullanılarak geliştirilmiş production odaklı bir ERP / CRM backend sistemidir.

Amaç; gerçekçi veri üretebilen, ölçeklenebilir, modüler ve sürdürülebilir bir backend mimarisi oluşturmaktır.

## Kullanılan Teknolojiler

- ASP.NET Core Web API
- Entity Framework Core
- MSSQL
- Clean Architecture
- CQRS
- MediatR
- JWT Authentication
- Refresh Token
- Role-based Authorization
- Serilog
- FluentValidation
- Bogus Fake Data Engine
- Swagger

## Mimari Yapı

```txt
ErpCrm.Backend
├── ErpCrm.API
├── ErpCrm.Application
├── ErpCrm.Domain
├── ErpCrm.Infrastructure
└── ErpCrm.Persistence
```

## Katmanlar

### ErpCrm.API
Controller’lar, middleware’ler, Swagger, authentication ve HTTP pipeline yapılandırmalarını içerir.

### ErpCrm.Application
CQRS command/query yapıları, DTO’lar, validator’lar, MediatR handler’ları, Result Pattern ve servis interface’lerini içerir.

### ErpCrm.Domain
Entity’ler, enum’lar ve temel domain modellerini içerir.

### ErpCrm.Infrastructure
JWT servisleri, password hashing, current user servisleri, fake data seeder ve dış servis entegrasyonlarını içerir.

### ErpCrm.Persistence
DbContext, Fluent API configuration dosyaları, migration’lar ve database initialization yapısını içerir.

## Modüller

- Users
- Roles
- Customers
- Categories
- Products
- Product Variants
- Product Images
- Warehouses
- Stocks
- Stock Movements
- Orders
- Order Items
- Payments
- Notifications
- Audit Logs

## Authentication & Authorization

Sistemde JWT tabanlı authentication yapısı bulunmaktadır.

Özellikler:

- Login
- Register
- Access Token
- Refresh Token
- Logout
- Role-based Authorization
- CurrentUserService

Roller:

```txt
Admin
Manager
Employee
```

## CQRS + MediatR

Sistem CQRS pattern ile geliştirilmiştir.

Her modül kendi command, query, handler, DTO ve validator dosyalarına sahiptir.

```txt
Features
└── Products
    ├── Commands
    ├── Queries
    ├── DTOs
    └── Validators
```

## Validation Pipeline

FluentValidation, MediatR pipeline içine entegre edilmiştir.

Akış:

```txt
Request
→ ValidationBehavior
→ Validator
→ Handler
→ Result
→ Controller
```

Bu sayede validation işlemleri controller veya handler içinde tekrarlanmaz.

## Global Exception Middleware

Tüm beklenmeyen hatalar merkezi middleware tarafından yakalanır.

Standart hata response formatı:

```json
{
  "success": false,
  "message": "Validation error",
  "errors": [
    "Email is required"
  ],
  "statusCode": 400,
  "traceId": "..."
}
```

## Result Pattern

Tüm handler response’ları standart `Result<T>` yapısı üzerinden döner.

Bu yapı API response’larını merkezi ve okunabilir hale getirir.

## Database

Veritabanı olarak MSSQL kullanılmaktadır.

Özellikler:

- EF Core
- Fluent API
- Proper foreign key ilişkileri
- Index optimizasyonları
- Soft delete
- UTC date kullanımı

Tüm temel entity’lerde şu alanlar bulunur:

```txt
CreatedDate
UpdatedDate
DeletedDate
IsDeleted
```

## Audit Logging

Audit sistemi önemli işlemleri takip etmek için hazırlanmıştır.

Loglanan işlemler:

- Create
- Update
- Delete
- Login
- Stock changes

AuditLog alanları:

```txt
UserId
Action
EntityName
OldValues
NewValues
IPAddress
CreatedDate
```

## Fake Data Engine

Bogus kullanılarak gerçekçi demo verileri üretilmektedir.

Mevcut fake data yapısı:

- Users
- Customers
- Categories
- Products
- Product Variants
- Product Images
- Warehouses
- Stocks
- Orders
- Payments
- Notifications
- Stock Movements

Simülasyon kuralları:

- Bazı müşteriler aktif, bazıları pasif oluşturulur.
- Bazı ürünler popüler olarak işaretlenir.
- Hafta sonu sipariş yoğunluğu artırılır.
- Gece saatlerinde daha az sipariş oluşturulur.
- Sipariş oluşunca stok düşer.
- Payment ve notification otomatik oluşur.

## Swagger

Swagger JWT authorization desteği ile yapılandırılmıştır.

Authorize butonu üzerinden Bearer token ile protected endpoint’ler test edilebilir.

## Performans Yaklaşımı

Uygulamada şu performans pratikleri kullanılmaktadır:

- AsNoTracking
- Projection
- DTO mapping
- Pagination
- Filtering
- Sorting
- Searching
- N+1 problemini önleme

## Çalıştırma

### Migration

```bash
dotnet ef database update --project ErpCrm.Persistence --startup-project ErpCrm.API
```

### API Çalıştırma

```bash
dotnet run --project ErpCrm.API
```

## Varsayılan Admin

```txt
Email: admin@erpcrm.com
Password: Admin123*
```

## Fake Data Seed

```http
POST /api/fakedata/seed
```

Admin yetkisi gerektirir.

## Mevcut Durum

Tamamlananlar:

- Clean Architecture
- CQRS
- JWT Auth
- Refresh Token
- CurrentUserService
- Role-based Authorization
- Result Pattern
- Global Exception Middleware
- FluentValidation Pipeline
- Serilog
- AuditLog
- Bogus Fake Data Seeder
- Product Variant & Image sistemi
- Order / Payment / Stock flow

Planlananlar:

- 10k users / 5k customers / 5k products / 100k orders massive seed
- Domain Events
- Event-driven workflow
- Redis Cache
- Hangfire Background Jobs
- Dashboard Analytics
- Health Checks
- Docker
- Unit & Integration Tests

---

# 🇬🇧 English Documentation

## Project Overview

This project is a production-oriented ERP / CRM backend system built with modern .NET technologies.

The main goal is to create a scalable, modular, maintainable backend system capable of generating realistic enterprise-level data.

## Technologies

- ASP.NET Core Web API
- Entity Framework Core
- MSSQL
- Clean Architecture
- CQRS
- MediatR
- JWT Authentication
- Refresh Token
- Role-based Authorization
- Serilog
- FluentValidation
- Bogus Fake Data Engine
- Swagger

## Architecture

```txt
ErpCrm.Backend
├── ErpCrm.API
├── ErpCrm.Application
├── ErpCrm.Domain
├── ErpCrm.Infrastructure
└── ErpCrm.Persistence
```

## Layers

### ErpCrm.API
Contains controllers, middlewares, Swagger configuration, authentication configuration and HTTP pipeline setup.

### ErpCrm.Application
Contains CQRS commands, queries, DTOs, validators, MediatR handlers, Result Pattern and service interfaces.

### ErpCrm.Domain
Contains entities, enums and core domain models.

### ErpCrm.Infrastructure
Contains JWT services, password hashing, current user services, fake data seeding and external service integrations.

### ErpCrm.Persistence
Contains DbContext, Fluent API configurations, migrations and database initialization.

## Modules

- Users
- Roles
- Customers
- Categories
- Products
- Product Variants
- Product Images
- Warehouses
- Stocks
- Stock Movements
- Orders
- Order Items
- Payments
- Notifications
- Audit Logs

## Authentication & Authorization

The system uses JWT-based authentication.

Features:

- Login
- Register
- Access Token
- Refresh Token
- Logout
- Role-based Authorization
- CurrentUserService

Roles:

```txt
Admin
Manager
Employee
```

## CQRS + MediatR

The system is developed using the CQRS pattern.

Each module has its own command, query, handler, DTO and validator files.

```txt
Features
└── Products
    ├── Commands
    ├── Queries
    ├── DTOs
    └── Validators
```

## Validation Pipeline

FluentValidation is integrated into the MediatR pipeline.

Flow:

```txt
Request
→ ValidationBehavior
→ Validator
→ Handler
→ Result
→ Controller
```

Validation logic is not repeated inside controllers or handlers.

## Global Exception Middleware

All unexpected exceptions are handled by a centralized middleware.

Standard error response:

```json
{
  "success": false,
  "message": "Validation error",
  "errors": [
    "Email is required"
  ],
  "statusCode": 400,
  "traceId": "..."
}
```

## Result Pattern

All handler responses use a standardized `Result<T>` structure.

This keeps API responses consistent and readable.

## Database

MSSQL is used as the database.

Features:

- EF Core
- Fluent API
- Proper foreign key relationships
- Index optimization
- Soft delete
- UTC date handling

All core entities include:

```txt
CreatedDate
UpdatedDate
DeletedDate
IsDeleted
```

## Audit Logging

The audit system tracks important actions.

Tracked actions:

- Create
- Update
- Delete
- Login
- Stock changes

AuditLog fields:

```txt
UserId
Action
EntityName
OldValues
NewValues
IPAddress
CreatedDate
```

## Fake Data Engine

Bogus is used to generate realistic demo data.

Current fake data structure:

- Users
- Customers
- Categories
- Products
- Product Variants
- Product Images
- Warehouses
- Stocks
- Orders
- Payments
- Notifications
- Stock Movements

Simulation rules:

- Some customers are active and some are passive.
- Some products are marked as popular.
- Weekend order density is increased.
- Fewer orders are generated at night.
- Stock is decreased when an order is created.
- Payment and notification records are created automatically.

## Swagger

Swagger is configured with JWT authorization support.

Protected endpoints can be tested using the Authorize button with a Bearer token.

## Performance Practices

The application uses the following performance practices:

- AsNoTracking
- Projection
- DTO mapping
- Pagination
- Filtering
- Sorting
- Searching
- N+1 prevention

## Running the Project

### Migration

```bash
dotnet ef database update --project ErpCrm.Persistence --startup-project ErpCrm.API
```

### Run API

```bash
dotnet run --project ErpCrm.API
```

## Default Admin

```txt
Email: admin@erpcrm.com
Password: Admin123*
```

## Fake Data Seed

```http
POST /api/fakedata/seed
```

Admin authorization is required.

## Current Status

Completed:

- Clean Architecture
- CQRS
- JWT Auth
- Refresh Token
- CurrentUserService
- Role-based Authorization
- Result Pattern
- Global Exception Middleware
- FluentValidation Pipeline
- Serilog
- AuditLog
- Bogus Fake Data Seeder
- Product Variant & Image system
- Order / Payment / Stock flow

Planned:

- 10k users / 5k customers / 5k products / 100k orders massive seed
- Domain Events
- Event-driven workflow
- Redis Cache
- Hangfire Background Jobs
- Dashboard Analytics
- Health Checks
- Docker
- Unit & Integration Tests

---

# License

This project is developed for educational and portfolio purposes.
