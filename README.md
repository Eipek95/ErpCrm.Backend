# ERP CRM Backend System

<div align="center">

![.NET](https://img.shields.io/badge/.NET-9.0-purple)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-blue)
![CQRS](https://img.shields.io/badge/Pattern-CQRS-green)
![Redis](https://img.shields.io/badge/Cache-Distributed%20Cache-red)
![MediatR](https://img.shields.io/badge/MediatR-Event%20Driven-orange)
![License](https://img.shields.io/badge/License-MIT-lightgrey)

Production-ready ERP / CRM Backend System built with modern .NET technologies.

</div>

---

<div align="center">

[🇹🇷 Türkçe](#-türkçe-dokümantasyon)
|
[🇬🇧 English](#-english-documentation)

</div>

---

# 🇹🇷 Türkçe Dokümantasyon

## 📌 Proje Hakkında

Bu proje modern .NET teknolojileri kullanılarak geliştirilmiş production odaklı bir ERP / CRM backend sistemidir.

Amaç:

* Ölçeklenebilir backend mimarisi oluşturmak
* Gerçek enterprise mimari pratiklerini uygulamak
* CQRS + Domain Events + Distributed Cache mimarisini kullanmak
* Büyük veri yükleri altında performans testleri yapabilmek
* Gerçek ERP/CRM senaryolarını modellemek

---

# 🏗 Kullanılan Mimariler

## Clean Architecture

```txt
Presentation
↓
Application
↓
Domain
↓
Infrastructure
↓
Persistence
```

## CQRS Architecture

```txt
Commands → Write Operations
Queries  → Read Operations
```

## Event Driven Architecture

```txt
CreateOrderCommand
↓
OrderCreatedEvent
↓
Handlers:
- CreatePayment
- CreateNotification
- CreateAuditLog
- InvalidateDashboardCache
```

---

# 🚀 Kullanılan Teknolojiler

* ASP.NET Core Web API
* Entity Framework Core
* MSSQL
* MediatR
* CQRS
* FluentValidation
* JWT Authentication
* Role Based Authorization
* Distributed Cache
* Memory Cache / Redis Ready
* Serilog
* Domain Events
* Soft Delete
* Global Exception Middleware
* Fake Data Generator

---

# 📊 Dashboard API'leri

## Dashboard Endpoints

```http
GET /api/dashboard/stats
GET /api/dashboard/monthly-sales
GET /api/dashboard/top-products
GET /api/dashboard/warehouse-stock
GET /api/dashboard/recent-orders
```

---

# ⚡ Distributed Cache Sistemi

Projede dashboard endpointleri distributed cache ile optimize edilmiştir.

## Cache Kullanılan Endpointler

| Endpoint        | Cache Key                       |
| --------------- | ------------------------------- |
| Dashboard Stats | dashboard:stats                 |
| Monthly Sales   | dashboard:monthly-sales:{year}  |
| Top Products    | dashboard:top-products:{count}  |
| Warehouse Stock | dashboard:warehouse-stock       |
| Recent Orders   | dashboard:recent-orders:{count} |

---

# 🔥 Cache Invalidation

Sipariş veya ödeme değişikliklerinde cache otomatik temizlenir.

## Event Tetiklenen Cache Temizleme Yapıları

```txt
OrderCreatedEvent
OrderCancelledEvent
PaymentCompletedEvent
```

## Otomatik Temizlenen Cache'ler

```txt
- dashboard:stats
- dashboard:monthly-sales
- dashboard:top-products
- dashboard:warehouse-stock
- dashboard:recent-orders
```

---

# 🧠 Domain Events

Sistem event-driven mimariye göre geliştirilmiştir.

## Kullanılan Domain Events

```txt
OrderCreatedEvent
OrderCancelledEvent
PaymentCompletedEvent
```

## Örnek Event Flow

```txt
CreateOrderCommand
↓
OrderCreatedEvent
↓
CreatePaymentHandler
↓
CreateNotificationHandler
↓
CreateAuditLogHandler
↓
InvalidateDashboardCacheHandler
```

---

# 🧪 Fake Data Engine

Projede massive data testleri için gelişmiş fake data sistemi bulunmaktadır.

## Özellikler

* Batch insert sistemi
* Progress logging
* Massive data generation
* Relationship aware seed structure
* Order / Stock / Payment generation
* Admin user auto creation
* Role based seed data

## Örnek Seed Config

```json
"FakeData": {
  "UserCount": 1000,
  "CustomerCount": 2000,
  "ProductCount": 3000,
  "OrderCount": 10000,
  "BatchSize": 1000
}
```

---

# 🔐 Authentication & Authorization

## JWT Authentication

```txt
Bearer Token Authentication
```

## Role Based Authorization

```txt
Admin
Manager
Employee
```

---

# 📁 Proje Yapısı

```txt
src/
 ├── ErpCrm.API
 ├── ErpCrm.Application
 ├── ErpCrm.Domain
 ├── ErpCrm.Infrastructure
 └── ErpCrm.Persistence
```

---

# 📌 Tamamlanan Sistemler

✅ Clean Architecture

✅ CQRS

✅ JWT Authentication

✅ Role Based Authorization

✅ Global Exception Middleware

✅ FluentValidation

✅ Dashboard Analytics APIs

✅ Distributed Cache

✅ Cache Invalidation

✅ Domain Events

✅ Fake Data Generator

✅ Audit Log System

✅ Soft Delete

✅ Stock Management

✅ Payment Management

---

# 🔜 Roadmap

## Yakında Eklenecekler

* Hangfire Background Jobs
* Low Stock Scheduled Jobs
* Docker Support
* Health Checks
* Massive Load Tests (100k+)
* SQL Index Optimization
* Outbox Pattern
* Integration Tests
* Frontend Admin Panel

---

# ▶️ Projeyi Çalıştırma

## Migration

```bash
add-migration InitialCreate
update-database
```

## Run API

```bash
dotnet run
```

## Swagger

```txt
https://localhost:5001/swagger
```

---

# 📬 API Documentation

Swagger UI üzerinden tüm endpointler test edilebilir.

---

# 🌍 English Documentation

## 📌 About The Project

This project is a production-ready ERP / CRM backend system developed using modern .NET technologies.

Goals:

* Build scalable backend architecture
* Apply real enterprise architecture patterns
* Use CQRS + Domain Events + Distributed Cache architecture
* Perform large-scale data load testing
* Simulate real ERP / CRM business scenarios

---

# 🏗 Architecture

## Clean Architecture

```txt
Presentation
↓
Application
↓
Domain
↓
Infrastructure
↓
Persistence
```

## CQRS Architecture

```txt
Commands → Write Operations
Queries  → Read Operations
```

## Event Driven Architecture

```txt
CreateOrderCommand
↓
OrderCreatedEvent
↓
Handlers:
- CreatePayment
- CreateNotification
- CreateAuditLog
- InvalidateDashboardCache
```

---

# 🚀 Technologies

* ASP.NET Core Web API
* Entity Framework Core
* MSSQL
* MediatR
* CQRS
* FluentValidation
* JWT Authentication
* Role Based Authorization
* Distributed Cache
* Memory Cache / Redis Ready
* Serilog
* Domain Events
* Soft Delete
* Global Exception Middleware
* Fake Data Generator

---

# 📊 Dashboard APIs

```http
GET /api/dashboard/stats
GET /api/dashboard/monthly-sales
GET /api/dashboard/top-products
GET /api/dashboard/warehouse-stock
GET /api/dashboard/recent-orders
```

---

# ⚡ Distributed Cache

Dashboard endpoints are optimized with distributed cache.

## Cached Endpoints

| Endpoint        | Cache Key                       |
| --------------- | ------------------------------- |
| Dashboard Stats | dashboard:stats                 |
| Monthly Sales   | dashboard:monthly-sales:{year}  |
| Top Products    | dashboard:top-products:{count}  |
| Warehouse Stock | dashboard:warehouse-stock       |
| Recent Orders   | dashboard:recent-orders:{count} |

---

# 🧠 Domain Events

The system is designed with event-driven architecture.

## Implemented Domain Events

```txt
OrderCreatedEvent
OrderCancelledEvent
PaymentCompletedEvent
```

---

# 🧪 Fake Data Engine

The project contains an advanced fake data engine for massive data testing.

## Features

* Batch insert system
* Progress logging
* Massive data generation
* Relationship aware seed structure
* Order / Stock / Payment generation
* Admin user auto creation
* Role based seed data

---

# 📌 Completed Systems

✅ Clean Architecture

✅ CQRS

✅ JWT Authentication

✅ Role Based Authorization

✅ FluentValidation

✅ Distributed Cache

✅ Cache Invalidation

✅ Domain Events

✅ Fake Data Generator

✅ Dashboard Analytics APIs

---

# 🔜 Roadmap

* Hangfire Background Jobs
* Low Stock Scheduled Jobs
* Docker Support
* Health Checks
* Massive Load Tests (100k+)
* SQL Index Optimization
* Outbox Pattern
* Integration Tests
* Frontend Admin Panel

---

# ▶️ Run The Project

## Migration

```bash
add-migration InitialCreate
update-database
```

## Run API

```bash
dotnet run
```

## Swagger

```txt
https://localhost:5001/swagger
```

---

# 👨‍💻 Developer

Developed for learning enterprise backend architecture, scalability and production-grade API design.
