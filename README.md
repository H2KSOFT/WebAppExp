# Sistema de Gestión de Ventas

## Descripción
Sistema moderno para gestión de órdenes construido con .NET 8, Clean Architecture, CQRS y JWT.

## Arquitectura
- **Domain**: Entidades y interfaces puras
- **Application**: Casos de uso, CQRS, validaciones
- **Infrastructure**: Entity Framework, Repositorios
- **Web.API**: API REST con JWT
- **Web.MVC**: Cliente MVC que consume la API

## Tecnologías
- .NET 8
- Entity Framework Core
- ASP.NET Core Web API
- ASP.NET Core MVC
- JWT Authentication
- SQL Server
- MediatR
- AutoMapper
- FluentValidation

## Características
- ✅ Autenticación JWT con roles
- ✅ CRUD completo de órdenes
- ✅ Validaciones con FluentValidation
- ✅ Patrón CQRS con MediatR
- ✅ Clean Architecture
- ✅ Documentación Swagger

## Ejecución
1. Configurar connection string en `Web.API/appsettings.json` y `Web.MVC/appsettings.json`
3. Ejecutar Web.API y Web.MVC