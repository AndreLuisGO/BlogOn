# BlogOn API

A modern, scalable blog API built with ASP.NET Core 8.0, demonstrating clean architecture principles, CQRS pattern, and vertical slice architecture.



## Future Development Roadmap (Improvements)

To elevate this system to an enterprise-grade production standard, the following enhancements are proposed:

- Authentication and Authorization: Integration with identity providers such as Auth0 or IdentityServer (utilizing JWT Bearer Tokens).

- Observability and Telemetry: Implementation of OpenTelemetry (Jaeger/Prometheus) to facilitate distributed tracing and performance monitoring.

- Comprehensive Testing: Expansion of the BlogApi.Tests suite to include robust Integration Tests utilizing TestContainers and WebApplicationFactory.

- Add Resiliency Patterns: Implement retry policies and circuit breakers using Polly to enhance fault tolerance.

- Rate Limiting: Introduce rate limiting middleware to protect the API from abuse and ensure fair usage.

- CI/CD Pipeline: Establish automated build, test, and deployment pipelines using GitHub Actions.

- Add Explicit CORS policies for safer production environment.


## Technology Stack

### Core Framework
- **ASP.NET Core 8.0** - Web API framework
- **.NET 8.0** - Runtime platform
- **C# 12** - Programming language

### Data Persistence
- **Entity Framework Core 8.0.11** - Object-relational mapping (ORM)
- **Microsoft SQL Server 2022** - Relational database
- **Dapper 2.1.66** - Micro-ORM for optimized read operations

### Architecture & Patterns
- **MediatR 13.1.0** - Mediator pattern implementation for CQRS
- **FluentValidation 12.1.0** - Input validation framework

### Caching & Performance
- **Redis (Alpine)** - Distributed caching layer
- **StackExchange.Redis 8.0.11** - Redis client for .NET

### API Documentation
- **Swashbuckle.AspNetCore 7.2.0** - Swagger/OpenAPI documentation
- **Microsoft.AspNetCore.OpenApi 8.0.11** - OpenAPI specification support

### Containerization
- **Docker** - Container platform
- **Docker Compose** - Multi-container orchestration

## Architecture Overview

### Design Patterns

#### Vertical Slice Architecture
The application is organized by feature rather than technical layer. Each feature contains all necessary components (handlers, validators, DTOs) in a single location, promoting high cohesion and low coupling.

```
Features/
├── Posts/
│   ├── CreatePost.cs       # Command handler for creating posts
│   ├── GetAllPosts.cs      # Query handler for listing posts
│   └── GetPostById.cs      # Query handler for post details
└── Comments/
    └── AddComment.cs       # Command handler for adding comments
```

#### CQRS (Command Query Responsibility Segregation)
Commands and queries are separated to optimize each operation independently:
- **Commands** use Entity Framework Core for write operations with full ORM benefits
- **Queries** use Dapper for optimized read operations with direct SQL control

#### Pipeline Behaviors
Cross-cutting concerns are implemented as MediatR pipeline behaviors:
- **ValidationBehavior** - Automatic request validation using FluentValidation
- **CachingBehavior** - Transparent caching for queries implementing `ICacheableQuery`

### Project Structure

```
BlogOnAPI/
├── Domain/                 # Domain entities
│   ├── BlogPost.cs
│   └── Comment.cs
├── Features/               # Vertical slices by feature
│   ├── Posts/
│   └── Comments/
├── Infrastructure/         # Cross-cutting concerns
│   ├── BlogDbContext.cs
│   ├── GlobalExceptionHandler.cs
│   └── Behaviors/
│       ├── CachingBehavior.cs
│       └── ValidationBehavior.cs
├── Application/
│   └── Interfaces/
│       └── ICacheableQuery.cs
└── Program.cs             # Application entry point
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/posts` | Retrieve all blog posts with comment counts |
| GET | `/api/posts/{id}` | Retrieve a specific post with all comments |
| POST | `/api/posts` | Create a new blog post |
| POST | `/api/posts/{id}/comments` | Add a comment to a blog post |

## Getting Started

### Prerequisites
- Docker Desktop installed and running
- .NET 8.0 SDK (for local development)

### Running with Docker (Recommended)

1. Clone the repository
2. Navigate to the project root directory
3. Start the application:

```powershell
docker-compose up --build -d
```

The API will be available at:
- **API Base URL**: http://localhost:5000
- **Swagger Documentation**: http://localhost:5000/swagger

### Running Locally (Development)

1. Update connection strings in `appsettings.json` to point to your local SQL Server and Redis instances
2. Apply database migrations:

```powershell
dotnet ef database update --project BlogOnAPI/BlogOnAPI.csproj
```

3. Run the application:

```powershell
dotnet run --project BlogOnAPI/BlogOnAPI.csproj
```

The API will be available at:
- **HTTPS**: https://localhost:54986
- **HTTP**: http://localhost:54987
- **Swagger**: https://localhost:54986/swagger

### Hot Reload for Development

For rapid development with automatic rebuild on file changes:

```powershell
dotnet watch run --project BlogOnAPI/BlogOnAPI.csproj
```

## Configuration

### Connection Strings

Connection strings are configured through environment variables in Docker Compose or `appsettings.json` for local development.

**Docker (docker-compose.yml)**:
```yaml
- ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=BlogDb;User Id=sa;Password=Strong!Password123;TrustServerCertificate=True;
- ConnectionStrings__Redis=redis:6379
```

**Local (appsettings.json)**:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BlogDb;Trusted_Connection=True;",
    "Redis": "localhost:6379"
  }
}
```

## Key Features

### Automatic Validation
All commands and queries are automatically validated using FluentValidation before processing. Invalid requests return detailed validation error responses.

### Intelligent Caching
Queries implementing the `ICacheableQuery` interface are automatically cached in Redis. Cache keys are generated based on query parameters, and entries include configurable time-to-live (TTL) settings.

### Global Exception Handling
A centralized exception handler provides consistent error responses across all endpoints, ensuring predictable error handling for API consumers.

### Database Seeding
The application includes seed data with three blog posts and associated comments, automatically created during database initialization.

## Database Migrations

### Create a New Migration

```powershell
dotnet ef migrations add MigrationName --project BlogOnAPI/BlogOnAPI.csproj
```

### Apply Migrations

```powershell
dotnet ef database update --project BlogOnAPI/BlogOnAPI.csproj
```

### Remove Last Migration

```powershell
dotnet ef migrations remove --project BlogOnAPI/BlogOnAPI.csproj
```

## Container Management

### View Logs

```powershell
docker logs blogon-api
docker logs blogon-sql
docker logs blogon-redis
```

### Stop Containers

```powershell
docker-compose down
```

### Rebuild and Restart

```powershell
docker-compose down
docker-compose up --build -d
```

## Best Practices Implemented

- **Separation of Concerns**: Commands and queries are isolated with distinct responsibilities
- **Single Responsibility Principle**: Each handler manages one specific operation
- **Dependency Injection**: All dependencies are injected through constructors
- **Configuration-based Settings**: Environment-specific settings are externalized
- **API Versioning Ready**: Structure supports future API versioning
- **Documentation First**: Comprehensive OpenAPI/Swagger documentation

## License

This project is intended for educational and demonstration purposes.
