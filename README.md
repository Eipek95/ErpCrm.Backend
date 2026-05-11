# ERP CRM Backend System

<div align="center">

![.NET](https://img.shields.io/badge/.NET-10.0-purple)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-blue)
![CQRS](https://img.shields.io/badge/Pattern-CQRS-green)
![Cache](https://img.shields.io/badge/Cache-Redis%20Distributed%20Cache-red)
![MediatR](https://img.shields.io/badge/MediatR-Event%20Driven-orange)
![SignalR](https://img.shields.io/badge/Realtime-SignalR-brightgreen)
![RabbitMQ](https://img.shields.io/badge/Message%20Broker-RabbitMQ-ff6600)
![Hangfire](https://img.shields.io/badge/Jobs-Hangfire-blue)
![License](https://img.shields.io/badge/License-MIT-lightgrey)

Production-ready ERP / CRM Backend System built with modern .NET technologies.

</div>

---

<div align="center">

[🇬🇧 English Documentation](#-english-documentation)  
[🇹🇷 Türkçe Dokümantasyon](#-türkçe-dokümantasyon)

</div>

---

# 🇬🇧 English Documentation

## 📌 About The Project

This project is a production-oriented ERP / CRM backend system developed with modern .NET technologies.

It is designed to simulate real-world enterprise backend scenarios such as customer management, product and product variant management, warehouse-based stock tracking, order management, payment workflow, dashboard analytics, real-time notifications, background jobs, refresh token authentication, Redis distributed cache and RabbitMQ message publishing.

This project is not only a CRUD-based backend. It also includes enterprise concepts such as CQRS, Domain Events, Redis Cache, SignalR, Hangfire, RabbitMQ publishing, rate limiting, request tracking, audit logging and large-scale data testing.

---

## 🏗 Architecture

```txt
API
↓
Application
↓
Domain
↓
Infrastructure
↓
Persistence
```

| Layer | Responsibility |
|---|---|
| API | Controllers, Middlewares, Hubs, Realtime services |
| Application | CQRS commands/queries, interfaces, DTOs, validators |
| Domain | Entities, enums, domain events, core business models |
| Infrastructure | Redis, RabbitMQ, JWT, external services |
| Persistence | DbContext, configurations, migrations, seed data |

---

## 🧱 Architectural Patterns

### CQRS

```txt
Commands → Create / Update / Delete operations
Queries  → Read / Reporting / Dashboard operations
```

### Domain Events

Implemented domain events:

```txt
OrderCreatedEvent
OrderCancelledEvent
PaymentCompletedEvent
```

Example flow:

```txt
CreateOrderCommand
↓
OrderCreatedEvent
↓
Payment creation
↓
Notification creation
↓
Audit logging
↓
Cache invalidation
↓
SignalR realtime event
↓
RabbitMQ message publish
```

---

## 🚀 Technologies

- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- SQL Server
- MediatR
- CQRS
- FluentValidation
- JWT Authentication
- Refresh Token Authentication
- Redis Distributed Cache
- SignalR
- RabbitMQ
- Hangfire
- Serilog
- Health Checks
- Rate Limiting
- Response Compression
- Swagger / OpenAPI
- Clean Architecture
- Domain Events
- Soft Delete
- Audit Logging
- Request Logging
- Fake Data Generator

---

## 📦 Product & Product Variant Management

The project includes a product structure suitable for ERP scenarios.

### Product Features

- Product name
- Description
- Price
- Category relation
- Active/passive status
- Created / updated dates
- Dashboard analytics integration
- Order system integration
- Stock system integration

### Product Variant Features

Product variants allow managing different combinations under the same product.

```txt
Product: T-Shirt
Variants:
- Black / Small
- Black / Medium
- White / Large
```

Supported logic:

```txt
✔ Variant-based stock
✔ Variant-based price
✔ Variant support in order items
✔ ProductVariantId support in stock records
✔ ProductVariantId support in order creation
```

---

## 🏬 Warehouse & Stock Management

```txt
✔ Warehouse-based stock tracking
✔ Reserved stock calculation
✔ Available stock calculation
✔ Product variant stock support
✔ Stock movement records
✔ Low stock detection
✔ Stock validation before order creation
✔ Low stock background job
✔ Realtime low stock alerts with SignalR
```

Stock flow:

```txt
Order created
↓
Product stock checked
↓
Warehouse stock checked
↓
Available quantity calculated
↓
Stock quantity decreased
↓
StockMovement record created
↓
Dashboard cache invalidated
```

---

## 🧾 Order Management

Order creation flow:

```txt
Customer validation
↓
User validation
↓
Product validation
↓
Variant validation
↓
Stock validation
↓
Order creation
↓
Order item creation
↓
Stock decrease
↓
Stock movement creation
↓
Payment creation
↓
Notification creation
↓
Audit log creation
↓
Domain event publish
↓
Dashboard cache invalidation
↓
SignalR realtime event
↓
RabbitMQ message publish
```

Enterprise concepts used:

```txt
✔ CQRS
✔ Domain Events
✔ Stock validation
✔ Payment workflow
✔ Notification workflow
✔ Audit logging
✔ Redis cache invalidation
✔ SignalR realtime notification
✔ RabbitMQ publishing
```

---

## 💳 Payment System

```txt
✔ Payment entity
✔ Payment status tracking
✔ Paid / Pending logic
✔ Payment completed domain event
✔ Dashboard analytics integration
✔ Cache invalidation after payment events
✔ Realtime notification support
```

Future improvements:

```txt
- iyzico / Stripe integration
- Bank transfer tracking
- Refund workflow
- Invoice generation
```

---

## 📊 Dashboard Analytics

```http
GET /api/dashboard/stats
GET /api/dashboard/monthly-sales
GET /api/dashboard/top-products
GET /api/dashboard/warehouse-stock
GET /api/dashboard/recent-orders
```

Dashboard features:

```txt
✔ Total orders
✔ Total sales
✔ Active customers
✔ Product count
✔ Low stock count
✔ Pending payments
✔ Monthly sales analytics
✔ Top selling products
✔ Warehouse stock analytics
✔ Recent orders
```

---

## ⚡ Redis Distributed Cache

| Endpoint | Cache Key |
|---|---|
| Dashboard Stats | dashboard:stats |
| Monthly Sales | dashboard:monthly-sales:{year} |
| Top Products | dashboard:top-products:{count} |
| Warehouse Stock | dashboard:warehouse-stock |
| Recent Orders | dashboard:recent-orders:{count} |

Cache invalidation events:

```txt
OrderCreatedEvent
OrderCancelledEvent
PaymentCompletedEvent
```

---

## 📡 SignalR Realtime System

```txt
✔ NotificationHub
✔ RealtimeNotificationService
✔ OnlineUserTracker
✔ Realtime order notifications
✔ Realtime low stock alerts
✔ Realtime notification events
✔ Online user count tracking
✔ Realtime test controller
```

Active realtime events:

```txt
OrderCreated
NotificationReceived
LowStockAlert
OnlineUserCountChanged
```

---

## 🐇 RabbitMQ Message Publishing

```txt
✔ RabbitMQ Docker container
✔ RabbitMQ Management UI
✔ IMessageBusService abstraction
✔ RabbitMqService implementation
✔ OrderCreatedMessage DTO
✔ PublishOrderCreatedToRabbitMqHandler
✔ order-created-queue
```

Current RabbitMQ flow:

```txt
OrderCreatedEvent
↓
PublishOrderCreatedToRabbitMqHandler
↓
IMessageBusService
↓
RabbitMQ
↓
order-created-queue
```

Current status:

```txt
✔ Message publishing implemented
❌ Consumer implementation postponed
```

---

## 🧵 Hangfire Background Jobs

Storage:

```txt
Hangfire SQL Server Storage
```

Active jobs:

```txt
LowStockCheckJob
NotificationCleanupJob
RefreshTokenCleanupJob
```

---

## 🔐 Authentication & Authorization

Implemented authentication features:

```txt
✔ JWT access token
✔ Refresh token table
✔ Refresh token rotation
✔ Logout / revoke refresh token
✔ Refresh token cleanup job
✔ CurrentUser middleware
✔ Role structure
```

Roles:

```txt
Admin
Manager
Employee
```

Authorization policies:

```txt
AdminOnly
ManagerOrAdmin
EmployeeOrAbove
```

---

## 📝 Logging & Observability

```txt
✔ Serilog console logging
✔ Serilog file logging
✔ RequestLoggingMiddleware
✔ RequestLogs table
✔ AuditLogs table
✔ Cache hit/miss logs
✔ Background job logs
✔ Seeder performance logs
✔ Domain event logs
```

---

## 🧪 Fake Data Engine & Large Scale Testing

Seeded data:

```txt
Users
Roles
Customers
Categories
Products
ProductVariants
Warehouses
Stocks
Orders
OrderItems
Payments
Notifications
AuditLogs
StockMovements
```

Large scale test:

```txt
✔ 100K order test completed
```

---

## ▶️ Run The Project

```bash
dotnet ef database update --project ErpCrm.Persistence --startup-project ErpCrm.API
dotnet run --project ErpCrm.API
```

Redis:

```powershell
docker run -d --name erpcrm-redis -p 6379:6379 redis
```

RabbitMQ:

```powershell
docker run -d --name erpcrm-rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

RabbitMQ UI:

```txt
http://localhost:15672
Username: guest
Password: guest
```

---

## 🔮 Future Improvements

```txt
- RabbitMQ consumer implementation
- Outbox Pattern
- Docker Compose
- Unit Tests
- Integration Tests
- CI/CD Pipeline
- Email Service
- File Upload System
- Advanced Reporting
- Redis Health Check
- RabbitMQ Health Check
- Multi Tenant Architecture
- API Versioning
- Frontend Integration
```

---

# 🇹🇷 Türkçe Dokümantasyon

## 📌 Proje Hakkında

Bu proje modern .NET teknolojileri kullanılarak geliştirilmiş production odaklı bir ERP / CRM backend sistemidir.

Sistem gerçek dünyadaki enterprise backend senaryolarını simüle etmek için tasarlanmıştır.

Bu kapsamda projede şu iş alanları modellenmiştir:

- Müşteri yönetimi
- Ürün ve ürün varyant yönetimi
- Depo bazlı stok takibi
- Sipariş yönetimi
- Ödeme akışı
- Dashboard analizleri
- Gerçek zamanlı bildirimler
- Arka plan işleri
- Refresh token kimlik doğrulama
- Redis distributed cache
- RabbitMQ mesaj yayınlama
- Request ve audit loglama
- Büyük veri testi

Bu proje sadece CRUD odaklı bir backend değildir. CQRS, Domain Events, Redis Cache, SignalR, Hangfire, RabbitMQ publishing, rate limiting ve request tracking gibi enterprise yapıları da içermektedir.

---

## 🏗 Mimari Yapı

```txt
API
↓
Application
↓
Domain
↓
Infrastructure
↓
Persistence
```

| Katman | Sorumluluk |
|---|---|
| API | Controller, middleware, hub ve realtime servisleri |
| Application | CQRS command/query, interface, DTO ve validator yapıları |
| Domain | Entity, enum, domain event ve temel iş modelleri |
| Infrastructure | Redis, RabbitMQ, JWT ve dış servisler |
| Persistence | DbContext, configuration, migration ve seed data |

---

## 🧱 Kullanılan Mimari Patternler

### CQRS

```txt
Command → Create / Update / Delete işlemleri
Query   → Listeleme / Raporlama / Dashboard işlemleri
```

### Domain Events

Kullanılan eventler:

```txt
OrderCreatedEvent
OrderCancelledEvent
PaymentCompletedEvent
```

Örnek akış:

```txt
CreateOrderCommand
↓
OrderCreatedEvent
↓
Payment oluşturma
↓
Notification oluşturma
↓
Audit log oluşturma
↓
Cache temizleme
↓
SignalR realtime event
↓
RabbitMQ message publish
```

---

## 🚀 Kullanılan Teknolojiler

- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- SQL Server
- MediatR
- CQRS
- FluentValidation
- JWT Authentication
- Refresh Token Authentication
- Redis Distributed Cache
- SignalR
- RabbitMQ
- Hangfire
- Serilog
- Health Checks
- Rate Limiting
- Response Compression
- Swagger / OpenAPI
- Clean Architecture
- Domain Events
- Soft Delete
- Audit Logging
- Request Logging
- Fake Data Generator

---

## 📦 Ürün ve Ürün Varyant Yönetimi

Projede ürün yapısı ERP senaryolarına uygun şekilde kurgulanmıştır.

### Ürün Özellikleri

- Ürün adı
- Açıklama
- Fiyat
- Kategori ilişkisi
- Aktif/pasif durumu
- Oluşturulma / güncellenme tarihleri
- Dashboard analizleriyle entegrasyon
- Sipariş sistemiyle entegrasyon
- Stok sistemiyle entegrasyon

### Ürün Varyant Özellikleri

Ürün varyantları aynı ürün altında farklı kombinasyonların yönetilmesini sağlar.

```txt
Ürün: Tişört
Varyantlar:
- Siyah / Small
- Siyah / Medium
- Beyaz / Large
```

Desteklenen yapılar:

```txt
✔ Varyant bazlı stok
✔ Varyant bazlı fiyat
✔ OrderItem içinde varyant desteği
✔ Stock kayıtlarında ProductVariantId desteği
✔ Sipariş oluştururken ProductVariantId desteği
```

---

## 🏬 Depo ve Stok Yönetimi

```txt
✔ Depo bazlı stok takibi
✔ Rezerve stok hesaplama
✔ Kullanılabilir stok hesaplama
✔ Ürün varyantı bazlı stok desteği
✔ Stok hareket kayıtları
✔ Kritik stok tespiti
✔ Sipariş öncesi stok doğrulama
✔ Low stock background job
✔ SignalR ile canlı düşük stok uyarıları
```

Stok akışı:

```txt
Sipariş oluşturulur
↓
Ürün stoğu kontrol edilir
↓
Depo stoğu kontrol edilir
↓
Kullanılabilir miktar hesaplanır
↓
Stok miktarı düşürülür
↓
StockMovement kaydı oluşturulur
↓
Dashboard cache temizlenir
```

---

## 🧾 Sipariş Yönetimi

Sipariş oluşturma akışı:

```txt
Müşteri doğrulama
↓
Kullanıcı doğrulama
↓
Ürün doğrulama
↓
Varyant doğrulama
↓
Stok doğrulama
↓
Sipariş oluşturma
↓
Sipariş kalemleri oluşturma
↓
Stok düşürme
↓
StockMovement oluşturma
↓
Payment oluşturma
↓
Notification oluşturma
↓
Audit log oluşturma
↓
Domain event publish
↓
Dashboard cache invalidation
↓
SignalR realtime event
↓
RabbitMQ message publish
```

Sipariş akışında kullanılan enterprise yapılar:

```txt
✔ CQRS
✔ Domain Events
✔ Stok doğrulama
✔ Payment workflow
✔ Notification workflow
✔ Audit logging
✔ Redis cache invalidation
✔ SignalR realtime notification
✔ RabbitMQ publishing
```

---

## 💳 Ödeme Sistemi

```txt
✔ Payment entity
✔ Payment status tracking
✔ Paid / Pending mantığı
✔ PaymentCompletedEvent
✔ Dashboard analytics entegrasyonu
✔ Payment event sonrası cache invalidation
✔ Realtime notification desteği
```

---

## 📊 Dashboard Analytics

```http
GET /api/dashboard/stats
GET /api/dashboard/monthly-sales
GET /api/dashboard/top-products
GET /api/dashboard/warehouse-stock
GET /api/dashboard/recent-orders
```

Dashboard özellikleri:

```txt
✔ Toplam sipariş
✔ Toplam satış
✔ Aktif müşteri sayısı
✔ Ürün sayısı
✔ Düşük stok sayısı
✔ Bekleyen ödeme sayısı
✔ Aylık satış analizi
✔ En çok satan ürünler
✔ Depo stok analizi
✔ Son siparişler
```

---

## ⚡ Redis Distributed Cache

| Endpoint | Cache Key |
|---|---|
| Dashboard Stats | dashboard:stats |
| Monthly Sales | dashboard:monthly-sales:{year} |
| Top Products | dashboard:top-products:{count} |
| Warehouse Stock | dashboard:warehouse-stock |
| Recent Orders | dashboard:recent-orders:{count} |

Cache temizleyen eventler:

```txt
OrderCreatedEvent
OrderCancelledEvent
PaymentCompletedEvent
```

---

## 📡 SignalR Realtime Sistemi

```txt
✔ NotificationHub
✔ RealtimeNotificationService
✔ OnlineUserTracker
✔ Canlı sipariş bildirimleri
✔ Canlı düşük stok uyarıları
✔ Canlı notification eventleri
✔ Online kullanıcı sayısı takibi
✔ Realtime test controller
```

Aktif realtime eventler:

```txt
OrderCreated
NotificationReceived
LowStockAlert
OnlineUserCountChanged
```

---

## 🐇 RabbitMQ Message Publishing

```txt
✔ RabbitMQ Docker container
✔ RabbitMQ Management UI
✔ IMessageBusService abstraction
✔ RabbitMqService implementation
✔ OrderCreatedMessage DTO
✔ PublishOrderCreatedToRabbitMqHandler
✔ order-created-queue
```

Mevcut RabbitMQ akışı:

```txt
OrderCreatedEvent
↓
PublishOrderCreatedToRabbitMqHandler
↓
IMessageBusService
↓
RabbitMQ
↓
order-created-queue
```

Mevcut durum:

```txt
✔ Message publishing tamamlandı
❌ Consumer implementation sonraya bırakıldı
```

---

## 🧵 Hangfire Background Jobs

Storage:

```txt
Hangfire SQL Server Storage
```

Aktif joblar:

```txt
LowStockCheckJob
NotificationCleanupJob
RefreshTokenCleanupJob
```

---

## 🔐 Authentication & Authorization

Uygulanan authentication özellikleri:

```txt
✔ JWT access token
✔ Refresh token tablosu
✔ Refresh token rotation
✔ Logout / refresh token revoke
✔ Refresh token cleanup job
✔ CurrentUser middleware
✔ Role yapısı
```

Roller:

```txt
Admin
Manager
Employee
```

Authorization policies:

```txt
AdminOnly
ManagerOrAdmin
EmployeeOrAbove
```

---

## 📝 Logging & Observability

```txt
✔ Serilog console logging
✔ Serilog file logging
✔ RequestLoggingMiddleware
✔ RequestLogs tablosu
✔ AuditLogs tablosu
✔ Cache hit/miss logları
✔ Background job logları
✔ Seeder performance logları
✔ Domain event logları
```

---

## 🧪 Fake Data Engine ve Büyük Veri Testi

Seed edilen veriler:

```txt
Users
Roles
Customers
Categories
Products
ProductVariants
Warehouses
Stocks
Orders
OrderItems
Payments
Notifications
AuditLogs
StockMovements
```

Büyük veri testi:

```txt
✔ 100K order testi tamamlandı
```

---

## ▶️ Projeyi Çalıştırma

```bash
dotnet ef database update --project ErpCrm.Persistence --startup-project ErpCrm.API
dotnet run --project ErpCrm.API
```

Redis:

```powershell
docker run -d --name erpcrm-redis -p 6379:6379 redis
```

RabbitMQ:

```powershell
docker run -d --name erpcrm-rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

RabbitMQ UI:

```txt
http://localhost:15672
Username: guest
Password: guest
```

---

## 🔮 Gelecek Geliştirmeler

```txt
- RabbitMQ consumer implementation
- Outbox Pattern
- Docker Compose
- Unit Tests
- Integration Tests
- CI/CD Pipeline
- Email Service
- File Upload System
- Advanced Reporting
- Redis Health Check
- RabbitMQ Health Check
- Multi Tenant Architecture
- API Versioning
- Frontend Integration
```
