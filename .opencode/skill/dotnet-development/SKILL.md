---
name: dotnet-development
description: Spezialisierte Anleitungen für .NET/C# Entwicklung mit Clean Architecture in diesem Projekt
---

# .NET Development Skill

## Projektübersicht

Dies ist ein ASP.NET Core 8 Web API Projekt nach Clean Architecture für Bierverwaltung.

## Architektur-Schichten

### 1. Präsentationsschicht (Beer2beer.API)
- Controllers mit `[Route("api/[controller]")]`
- Middleware für Request/Response Logging
- DI-Registrierung in `ServiceExtension.cs`

### 2. Dominbschicht (Beer2beer.Core)
- Entities mit `Base<T>` Basisklasse
- Interfaces für Services und Repositories
- Business Logic in Services
- Mapping mit `IBaseMapper<TSrc, TDest>`

### 3. Datenzugriffsschicht (Beer2beer.Infrastructure)
- `ApplicationDbContext` mit SQLite
- Repository Pattern mit `BaseRepository<T>`
- EF Core Migrations

## Entwicklung Workflows

### Neue Entity erstellen

1. Entity in `Core/Entities/` erstellen
```csharp
[Table("Products")]
public class Product : Base<int>
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
```

2. ViewModel erstellen
```csharp
public class ProductViewModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
```

3. Repository Interface in `Core/Interfaces/`
```csharp
public interface IProductRepository : IBaseRepository<Product> { }
```

4. Service Interface in `Core/Interfaces/`
```csharp
public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetProducts();
    Task<ProductViewModel> GetProduct(int id);
    Task<ProductViewModel> Create(ProductViewModel model);
    Task Update(ProductViewModel model);
    Task Delete(int id);
}
```

5. Repository Implementation in `Infrastructure/Repositories/`
```csharp
public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
```

6. Service Implementation in `Core/Services/`
```csharp
public class ProductService : IProductService
{
    private readonly IBaseMapper<Product, ProductViewModel> _viewModelMapper;
    private readonly IBaseMapper<ProductViewModel, Product> _entityMapper;
    private readonly IProductRepository _repository;

    // Constructor mit DI
    
    // Methoden implementieren
}
```

7. Controller in `API/Controllers/`
```csharp
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;

    // Constructor mit DI
    
    // CRUD Endpoints implementieren
}
```

8. DI in `ServiceExtension.cs` registrieren
```csharp
// Service
services.AddScoped<IProductService, ProductService>();

// Repository
services.AddTransient<IProductRepository, ProductRepository>();

// Mapper
cfg.CreateMap<Product, ProductViewModel>();
cfg.CreateMap<ProductViewModel, Product>();
services.AddSingleton<IBaseMapper<Product, ProductViewModel>>(new BaseMapper<Product, ProductViewModel>(mapper));
services.AddSingleton<IBaseMapper<ProductViewModel, Product>>(new BaseMapper<ProductViewModel, Product>(mapper));
```

9. DbSet in `ApplicationDbContext` hinzufügen
```csharp
public DbSet<Product> Products { get; set; }
```

10. Migration erstellen
```bash
dotnet ef migrations add AddProductTable --project Beer2beer.Infrastructure --startup-project Beer2beer.API
```

### Testing

```bash
# Build
dotnet build Beer2beer.sln

# Tests ausführen
dotnet test Beer2beer.UnitTest/Beer2beer.UnitTest.csproj

# Clean
dotnet clean Beer2beer.sln
```

## Best Practices

### Coding Standards
- PascalCase für Klassen und Methoden
- camelCase für lokale Variablen
- `_` Prefix für private Felder
- Interface Prefix `I`
- Async/await für alle I/O-Operationen

### Exception Handling
```csharp
try
{
    var result = await _service.GetData(id);
    return Ok(result);
}
catch (NotFoundException ex)
{
    return NotFound(ex.Message);
}
catch (Exception ex)
{
    _logger.LogError(ex, "Error message");
    return StatusCode(500, ex.Message);
}
```

### Validierung
- Data Annotations auf Entities und ViewModels
- `ModelState.IsValid` in Controllern prüfen
- Eindeutige Constraints (z.B. Email) prüfen

### Logging
- `ILogger<T>` in Constructor injecten
- Strukturiertes Logging mit Vorlagen
- Exception-Details loggen

## NuGet-Pakete

### Wichtige Pakete
- **AutoMapper 13.0.1**: Object-to-Object Mapping
- **EF Core 8.0.2**: ORM für Datenbankzugriff
- **Identity 8.0.2**: Authentication und Authorization
- **Swagger 6.5.0**: API-Dokumentation
- **Seq.Extensions.Logging 8.0.0**: Structured Logging

## Bekannte Einschränkungen

- SQLite als Datenbank (nur Development)
- Kein Authentication/Authorization implementiert
- Einige Services haben `NotImplementedException`
- Product/Order Domänen sind auskommentiert
