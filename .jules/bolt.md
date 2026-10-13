## 2025-05-18 - Avoid Database.EnsureCreated() in DbContext Constructor
**Learning:** Calling `Database.EnsureCreated()` inside a `DbContext` constructor causes EF Core to execute database schema inspection queries on every request because `DbContext` is scoped per HTTP request.
**Action:** Move database initialization (`Database.EnsureCreated()` or migrations) to application startup in `Program.cs` using an explicit `IServiceScope`.
