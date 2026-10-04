## 2025-05-20 - Avoid Calling `Database.EnsureCreated()` in `DbContext` Constructor

**Learning:** Calling `Database.EnsureCreated()` or performing schema checks inside the `DbContext` constructor causes EF Core to execute database schema inspection queries every time a `DbContext` instance is resolved (which occurs on every HTTP request for scoped services). This adds significant unnecessary latency and I/O overhead to every API request.
**Action:** Always perform database initialization, schema verification, or migrations once at application startup in `Program.cs` within a temporary `IServiceScope`, keeping `DbContext` constructors free of side effects.
