## 2025-05-18 - Avoid calling Database.EnsureCreated() in DbContext Constructor

**Learning:** Calling `Database.EnsureCreated()` (or `Database.Migrate()`) inside the `DbContext` constructor causes EF Core to issue schema queries on every single `DbContext` instantiation. Because `DbContext` is typically registered as `Scoped` in ASP.NET Core, this adds heavy, redundant database metadata query and file/disk overhead to every HTTP request.

**Action:** Perform database initialization/migration (`Database.EnsureCreated()`) once during application startup in `Program.cs` inside a temporary `IServiceScope`, keeping `DbContext` instantiation lightweight per HTTP request.
