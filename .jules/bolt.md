## 2025-02-18 - Database.EnsureCreated in DbContext Constructor Anti-pattern
**Learning:** Calling `Database.EnsureCreated()` inside a `DbContext` constructor causes EF Core to execute database schema check queries on every single `DbContext` instantiation. Since `DbContext` is scoped per HTTP request in ASP.NET Core Web APIs, this adds significant DB I/O overhead to every incoming API request.
**Action:** Always perform database initialization/migrations in an application startup service scope (`Program.cs`) rather than inside the `DbContext` constructor.
