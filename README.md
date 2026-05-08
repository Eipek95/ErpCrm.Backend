# ERP CRM Backend System

> 🇹🇷 Türkçe açıklamalar için aşağıdaki bölümleri kullanabilirsiniz.
>
> 🇬🇧 English explanations are available throughout the document.

---

# 🇹🇷 Türkçe Özet

## Proje Hakkında

Bu proje, modern .NET teknolojileri kullanılarak geliştirilmiş production odaklı bir ERP / CRM backend sistemidir.

Kullanılan temel teknolojiler:

* ASP.NET Core Web API
* Clean Architecture
* CQRS
* MediatR
* Entity Framework Core
* MSSQL
* JWT Authentication
* Serilog
* FluentValidation
* Bogus Fake Data Engine

## Mevcut Özellikler

* JWT Authentication & Refresh Token
* Role-based authorization
* CQRS + MediatR mimarisi
* FluentValidation pipeline
* Global exception middleware
* Result pattern
* Audit logging sistemi
* Fake data generator
* Product variant & image sistemi
* Soft delete desteği
* Pagination / filtering / sorting / searching
* Serilog logging
* Swagger JWT integration

## Modüller

* Users
* Roles
* Customers
* Products
* Categories
* Warehouses
* Stocks
* Orders
* Payments
* Notifications
* AuditLogs

## Mimari

```txt
src
├── ErpCrm.API
├── ErpCrm.Application
├── ErpCrm.Domain
├── ErpCrm.Infrastructure
└── ErpCrm.Persistence
```

## Planlanan Özellikler

* Massive scale seeding (10k+ users / 100k+ orders)
* Redis cache
* Hangfire background jobs
* Domain events
* Dashboard analytics
* Docker support
* Unit & integration tests

---

# 🇬🇧 English Documentation

Production-oriented ERP / CRM backend system built with modern .NET architecture principles.

---

# Overview

This project is a scalable ERP/CRM backend application developed using:

* ASP.NET Core Web API
* Clean Architecture
* CQRS Pattern
* MediatR
* Entity Framework Core
* MSSQL
* JWT Authentication
* Serilog Logging
* FluentValidation
* Bogus Fake Data Generator

The goal of this project is to simulate a real-world enterprise backend system with modular architecture, maintainability, scalability, and production-ready practices.

---

# Architecture

The project follows Clean Architecture principles.

## Layers

```txt
src
├── ErpCrm.API
├── ErpCrm.Application
├── ErpCrm.Domain
├── ErpCrm.Infrastructure
└── ErpCrm.Persistence
```

## Responsibilities

### ErpCrm.API

* Controllers
* Middlewares
* Authentication configuration
* Swagger configuration
* API pipeline

### ErpCrm.Application

* CQRS Commands & Queries
* DTOs
* Validators
* MediatR Handlers
* Interfaces
* Behaviors
* Result Pattern

### ErpCrm.Domain

* Entities
* Enums
* Base entities
* Domain models

### ErpCrm.Infrastructure

* JWT services
* Password hashing
* Current user services
* Logging integrations
* Fake data seeding

### ErpCrm.Persistence

* DbContext
* Entity configurations
* Migrations
* Fluent API mappings
* Database initialization

---

# Features

## Authentication & Authorization

* JWT Authentication
* Refresh Token support
* Role-based authorization
* Current user service
* Secure password hashing

### Roles

* Admin
* Manager
* Employee

---

# Modules

Implemented core ERP/CRM modules:

* Users
* Roles
* Customers
* Categories
* Products
* Product Variants
* Product Images
* Warehouses
* Stocks
* Stock Movements
* Orders
* Order Items
* Payments
* Notifications
* Audit Logs

---

# CQRS + MediatR

The system uses CQRS architecture with MediatR.

Each feature contains:

```txt
Features
└── Products
    ├── Commands
    ├── Queries
    ├── DTOs
    ├── Validators
    └── Handlers
```

Benefits:

* Separation of read/write logic
* Better scalability
* Cleaner architecture
* Easier maintenance
* Better testability

---

# Validation Pipeline

FluentValidation is integrated into MediatR pipeline behavior.

Validation runs automatically before handlers.

```txt
Request
→ ValidationBehavior
→ Validators
→ Handler
```

No validation logic exists inside controllers.

---

# Global Exception Handling

Custom global exception middleware provides:

* Standardized API responses
* Validation error handling
* TraceId support
* Centralized exception logging

## Standard Error Response

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

---

# Result Pattern

All handlers return standardized results.

```csharp
Result<T>
```

Benefits:

* Consistent API responses
* Better error handling
* Cleaner controller logic

---

# Database Design

## MSSQL + EF Core

* Entity Framework Core
* Fluent API configurations
* Proper foreign key relationships
* Index optimizations
* Soft delete support
* UTC date handling

## Base Entity Fields

All entities include:

```csharp
CreatedDate
UpdatedDate
DeletedDate
IsDeleted
```

---

# Logging

Serilog is configured for:

* Request logging
* Error logging
* File logging
* Console logging

Logs are stored in:

```txt
Logs/log-yyyyMMdd.txt
```

---

# Audit Logging

Automatic audit logging system tracks:

* Create operations
* Update operations
* Delete operations
* Login actions
* Stock movements

Audit data includes:

```txt
UserId
Action
EntityName
OldValues
NewValues
IPAddress
CreatedDate
```

---

# Fake Data Engine

Bogus is used to generate realistic ERP/CRM data.

## Current Seeder Features

* Realistic users
* Realistic customers
* Product categories
* Products with variants
* Product images
* Warehouses
* Stocks
* Orders
* Payments
* Notifications
* Stock movements

## Simulation Rules

* Some customers are active/passive
* Some products are popular
* Weekend orders are more frequent
* Night orders are less frequent
* Payments are distributed realistically
* Stock decreases automatically during order generation

---

# Swagger

Swagger includes:

* JWT authorization support
* API documentation
* Bearer token authentication

---

# Performance Practices

Implemented optimization practices:

* AsNoTracking
* Projection-based queries
* Pagination
* Filtering
* Sorting
* Searching
* N+1 prevention
* DTO projection

---

# Current Progress

Completed:

* Clean Architecture setup
* CQRS architecture
* JWT auth system
* Refresh token system
* Role-based authorization
* Global exception middleware
* Validation pipeline
* Audit logging
* Fake data seeding
* Product variant & image system
* ERP entities and relationships

Planned:

* Massive data generation (10k users / 100k orders)
* Domain events
* Event-driven workflows
* Redis caching
* Hangfire background jobs
* Dashboard analytics
* Docker support
* Unit & integration tests
* Health checks
* API versioning

---

# Technologies

## Backend

* ASP.NET Core Web API
* Entity Framework Core
* MSSQL
* MediatR
* FluentValidation
* Serilog
* Bogus
* Swagger

## Architecture

* Clean Architecture
* CQRS
* SOLID Principles
* Dependency Injection
* Feature-based folder structure

---

# Running the Project

## 1. Clone repository

```bash
git clone <repository-url>
```

## 2. Configure connection string

Update:

```txt
appsettings.json
```

## 3. Run migrations

```bash
dotnet ef database update --project ErpCrm.Persistence --startup-project ErpCrm.API
```

## 4. Run project

```bash
dotnet run --project ErpCrm.API
```

---

# Fake Data Seed

Use fake data endpoint:

```http
POST /api/fakedata/seed
```

Admin authorization required.

---

# Default Admin Account

```txt
Email: admin@erpcrm.com
Password: Admin123*
```

---

# API Features

List endpoints support:

* Pagination
* Searching
* Filtering
* Sorting

Example:

```http
GET /api/products?page=1&pageSize=20&search=laptop&sortBy=name
```

---

# Project Goals

This project aims to demonstrate:

* Enterprise backend architecture
* Scalable ERP system design
* Production-ready backend practices
* Modern .NET backend engineering
* Real-world business workflows

---

# License

This project is developed for educational and portfolio purposes.

---

# Author

Developed with modern enterprise backend architecture practices using .NET ecosystem.
