# Beer2beer - Agent Instructions

## Projektübersicht
ASP.NET Core 8 Web API nach Clean Architecture für Bierverwaltung (in Entwicklung).

## Architektur
- **Beer2beer.API**: Präsentation (Controllers, Middleware, DI)
- **Beer2beer.Core**: Business Logic (Entities, Services, Interfaces)
- **Beer2beer.Infrastructure**: Datenzugriff (EF Core, SQLite, Repositories)

## Entwicklungsbefehle

### Build & Test
```bash
dotnet build Beer2beer.sln
dotnet test Beer2beer.UnitTest/Beer2beer.UnitTest.csproj
dotnet clean Beer2beer.sln
```

### Entity Framework
```bash
# Migration erstellen
dotnet ef migrations add <MigrationName> --project Beer2beer.Infrastructure --startup-project Beer2beer.API

# Migration anwenden
dotnet ef database update --project Beer2beer.Infrastructure --startup-project Beer2beer.API
```

### Swagger
```bash
# API-Dokumentation anzeigen
start http://localhost:5000/swagger
```

## Coding-Guidelines

### Entities
- Von `Base<T>` erben (Id, EntryDate, UpdateDate)
- `[Table("TableName")]` Attribut verwenden
- Data Annotations für Validierung

### Repository Pattern
- Interfaces in `Core/Interfaces/`
- Implementation in `Infrastructure/Repositories/`
- Von `BaseRepository<T>` erben

### Services
- Business Logic in `Core/Services/`
- Interfaces in `Core/Interfaces/`
- Mapping über `IBaseMapper<TSrc, TDest>`

### Controller
- `ControllerBase` erben
- `[Route("api/[controller]")]` und `[ApiController]`
- Logging mit `ILogger<T>`
- Exception Handling mit try/catch

### Naming Conventions
- PascalCase für Klassen/Methoden
- camelCase für Variablen
- `_` Prefix für private Felder
- Interface Prefix `I`

## Bekannte Probleme
- Einige Services haben `NotImplementedException` (AppUserService)
- Product/Order Domänen sind auskommentiert
- SQLite als Datenbank (nur Development)

## NuGet-Pakete
- AutoMapper 13.0.1
- EF Core 8.0.2
- Identity 8.0.2
- Swagger 6.5.0
- Seq Logging 8.0.0
