# Total Solution

This repository consists of two main:

---

## 1. Main Framework

The core framework provides foundational building blocks, which you can extend and customize for your service, and then use them to meet your specific needs.

- Generic Repository and Unit of Work patterns with their signatures and implementations  
- AutoMapper basements  
- Various schemas: from base entities to structured request/response patterns
- Extenstions for enums, repo filters, and etc.
- Public utilities

---

## 2. Implemented Services (Sales Service)

Currently implemented services:

- The **Sales Service** leverages the **CQRS** pattern, implemented using *MediatR* for handling commands and queries. 

### Features

- Dependency Injection implementation  
- FluentValidation for input validation  
- Entity Framework ORM for data persistence  
- Database migration process implemented
- Apply data transfer objects, implemented using Request/Response classes and AutoMapper for seamless automatic mapping
- Repository and UOW
- Swagger Doc

---

### Database Migration Commands

Run the following commands in **Package Manager Console**:

```powershell
Add-Migration [MigrationName] 
Update-Database 
```
---

## Test Scenarios

Two sample scenarios have been tested:

- ✅ **Successful order creation**  
- ❌ **Order creation with validation errors**

