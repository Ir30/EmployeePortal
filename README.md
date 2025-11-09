🧾 Project Description — Employee Portal (.NET 8 Clean Architecture)

The Employee Portal is a modular and extensible backend system developed using ASP.NET Core 8, following the Clean Architecture principles.
It serves as a foundation for mastering enterprise-grade application development in .NET — demonstrating proper separation of concerns, scalable design, and maintainable coding practices.

🏗️ Architecture Overview

The project is built using the Clean Architecture pattern, ensuring that business logic remains independent of frameworks, UI, and data access technologies.

EmployeePortal/
│
├── EmployeePortal.API/             → Presentation Layer (Controllers, Middleware, Startup)
│   ├── Controllers/                → Handles incoming HTTP requests
│   ├── Middleware/                 → Global exception and request logging
│   └── Program.cs                  → Application startup and DI configuration
│
├── EmployeePortal.Application/     → Business Logic Layer
│   ├── DTOs/                       → Data Transfer Objects for clean communication
│   ├── Interfaces/                 → Contracts for repositories and services
│   ├── Services/                   → Core business operations
│
├── EmployeePortal.Domain/          → Core Domain Layer
│   ├── Entities/                   → Business entities (User, Department, Employee)
│   ├── Enums/                      → Domain enums (e.g., Roles)
│
├── EmployeePortal.Infrastructure/  → Data Access & Integration Layer
│   ├── Data/                       → EF Core DbContext and configuration
│   ├── Repositories/               → Implementations of repository interfaces
│   └── Services/                   → External or infrastructure-related services

⚙️ Current Features
🔐 Authentication & Authorization

JWT-based authentication with 1-hour expiry.

Role-based access control (Admin, User).

Secure password hashing using SHA256.

Custom JSON error messages for unauthorized and unauthenticated users.

👥 User Management

Create, read, update, and delete users.

Role assignment using enums.

Admin-only access for sensitive operations.

🏢 Department & Employee Management

Full CRUD operations.

Department–Employee relationship defined via Fluent API.

DTO-based request models for clean separation between data and logic.

⚡ Middleware

Exception Handling Middleware: Captures all unhandled exceptions and returns a clean JSON response.

Request Logging Middleware: Logs user, role, request path, and IP after authentication.

🧩 API Documentation

Fully integrated Swagger UI for testing and documentation.

Supports JWT token authentication directly in Swagger.

🧠 Architecture Features

Repository + Service pattern for clear separation of data and logic.

Strongly typed dependency injection.

Code-first EF Core migrations with MySQL.

Clean DTO mapping and validation.

🧱 Technology Stack


| Category         | Technology                                 |
| ---------------- | ------------------------------------------ |
| Framework        | ASP.NET Core 8                             |
| Database         | MySQL (via Entity Framework Core)          |
| Authentication   | JWT Bearer Tokens                          |
| Logging          | Built-in + Serilog (planned)               |
| Caching          | InMemory / Redis (planned)                 |
| Patterns         | Repository, Service Layer, DTO, Middleware |
| Future Additions | CQRS, MediatR, Unit Tests, Background Jobs |


🔄 Development Notes

This project is actively evolving and designed to grow as new .NET concepts are implemented.
The architecture, folder structure, and services have been intentionally designed for easy expansion.

Planned future additions include:
✅ Refresh Tokens and Logout Flow
✅ CQRS with MediatR
✅ Distributed Caching (Redis)
✅ Structured Logging (Serilog)
✅ Unit and Integration Testing (xUnit, Moq)
✅ Background Jobs and Domain Events
✅ Dockerization and Cloud Deployment (Azure)
Each new feature will be implemented following Clean Architecture principles and added to this documentation when complete.

🧭 Purpose

This project’s goal is to:
Serve as a learning reference for mastering Clean Architecture in .NET.
Demonstrate enterprise-level design patterns (DI, Middleware, CQRS, etc.).
Provide a scalable base project for future professional or academic projects.

🧑‍💻 Summary

The Employee Portal is not just a CRUD app — it’s a living project designed to evolve with your .NET mastery.
Built with clean layering, dependency injection, and real-world patterns, it provides a strong foundation for developing secure, maintainable, and scalable enterprise applications
