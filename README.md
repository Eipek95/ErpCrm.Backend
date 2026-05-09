# ERP CRM Backend System

<div align="center">

![.NET](https://img.shields.io/badge/.NET-10.0-purple)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-blue)
![CQRS](https://img.shields.io/badge/Pattern-CQRS-green)
![Cache](https://img.shields.io/badge/Cache-Distributed%20Cache-red)
![MediatR](https://img.shields.io/badge/MediatR-Event%20Driven-orange)
![License](https://img.shields.io/badge/License-MIT-lightgrey)

Production-ready ERP / CRM Backend System built with modern .NET technologies.

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
* .NET 10
* Entity Framework Core
* MSSQL
* MediatR
* CQRS
* FluentValidation
* JWT Authentication
* Distributed Cache
* Memory Cache / Redis Ready
* Serilog
* Domain Events
* Hangfire
* Soft Delete
* Global Exception Middleware
* Fake Data Generator
* Health Checks

---

# 🏢 Enterprise Features

## Implemented Enterprise Concepts

```txt
✔ Clean Architecture
✔ CQRS
✔ Domain Events
✔ Distributed Cache
✔ Cache Invalidation
✔ Background Jobs
✔ Health Checks
✔ Fake Massive Data Engine
✔ Audit Logging
✔ Soft Delete
✔ Stock Reservation Logic
✔ Payment Workflow
✔ Dashboard Analytics
✔ SQL Index Optimization
✔ Batch Processing
✔ Event Driven Architecture
```

---

# ⚙️ System Flow

## Order Creation Flow

```txt
CreateOrderCommand
        ↓
Stock Validation
        ↓
Stock Reservation
        ↓
Order Creation
        ↓
Payment Creation
        ↓
Notification Creation
        ↓
Audit Log Creation
        ↓
Domain Event Publish
        ↓
Dashboard Cache Invalidation
```

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

# 📦 Stock Management Logic

The project includes real ERP stock management scenarios.

## Features

```txt
✔ Warehouse-based stock tracking
✔ Reserved stock calculation
✔ Low stock detection
✔ Stock movement history
✔ Order-based stock reduction
✔ Product variant stock support
✔ Critical stock monitoring
```

---

# 📈 Analytics System

Dashboard analytics APIs are optimized for high-volume data access.

## Analytics Features

```txt
✔ Monthly sales analytics
✔ Warehouse stock analytics
✔ Top selling products
✔ Recent order tracking
✔ Revenue calculations
✔ Active customer statistics
✔ Pending payment monitoring
```

---

# 🧵 Background Job System

The project includes scheduled background processing using Hangfire.

## Active Jobs

### LowStockCheckJob

```txt
Checks critical stock levels
Creates notifications automatically
Runs periodically
```

### NotificationCleanupJob

```txt
Cleans old notifications
Applies soft delete logic
Runs daily
```

---

# 🩺 Health Monitoring

The API contains health monitoring endpoints.

## Health Endpoint

```http
GET /health
```

## Checks

```txt
✔ SQL Server connectivity
✔ API status
✔ Cache availability
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

# 🚀 Performance Optimizations

The backend is optimized for large-scale ERP operations.

## Implemented Optimizations

```txt
✔ AsNoTracking queries
✔ Distributed cache
✔ Batch save operations
✔ Projection queries
✔ SQL indexing
✔ Query optimization
✔ Event-based cache invalidation
✔ Lightweight dashboard DTOs
✔ Dapper-ready architecture
```

---

# 🗄 SQL Index Optimization

Critical database indexes were added for performance.

## Indexed Tables

```txt
Orders
OrderItems
Products
Customers
Stocks
Payments
Notifications
AuditLogs
StockMovements
```

---

# 🔍 Logging & Observability

The project includes detailed structured logging.

## Logging Features

```txt
✔ Cache hit/miss logs
✔ Batch save duration logs
✔ Background job logs
✔ Request logs
✔ Error logs
✔ Seeder performance logs
✔ Domain event logs
```

---

# 🔐 Authentication & Authorization

## JWT Authentication

```txt
Bearer Token Authentication
```

## Planned Role Structures

```txt
Admin
Manager
Employee
```

## Planned Authorization Policies

```txt
AdminOnly
ManagerOrAdmin
EmployeeOrAbove
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

✅ Hangfire Jobs

✅ Health Checks

✅ SQL Index Optimization

---

# 🧪 Large Scale Data Testing

The project is designed for massive ERP load simulations.

## Planned Load Tests

```txt
10K Orders
50K Orders
100K Orders
1M+ Future Goal
```

## Seeder Capabilities

```txt
✔ Massive order generation
✔ Warehouse stock generation
✔ Fake payment generation
✔ Notification generation
✔ Relationship-aware data creation
✔ Batch insert support
```

---

# 🔮 Planned Enterprise Features

## Future Improvements

```txt
✔ Redis Distributed Cache
✔ RabbitMQ Integration
✔ Outbox Pattern
✔ SignalR Realtime Notifications
✔ Docker Support
✔ Kubernetes Deployment
✔ CI/CD Pipelines
✔ Integration Testing
✔ Unit Testing
✔ Multi Tenant Architecture
✔ File Storage Service
✔ Email Queue System
✔ Refresh Token Flow
✔ Permission-based Authorization
```

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

# 🧠 Architectural Goals

This project aims to simulate real-world enterprise backend development practices.

## Main Goals

```txt
✔ Enterprise backend architecture
✔ Scalable ERP infrastructure
✔ High-volume data optimization
✔ Event-driven system design
✔ Production-grade API development
✔ Modern .NET backend practices
✔ Distributed system preparation
```

---

# 👨‍💻 Project Purpose

This project is being developed to improve knowledge in:

* Enterprise software architecture
* Scalable backend systems
* ERP/CRM business logic
* High-performance API development
* Distributed system design
* Event-driven architectures
* Production-ready .NET backend development
