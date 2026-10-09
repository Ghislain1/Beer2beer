## 2025-02-18 - Avoid Database.EnsureCreated() in DbContext Constructor

**Learning:** Calling `Database.EnsureCreated()` or `Database.Migrate()` inside a scoped `DbContext` constructor causes EF Core to inspect database schema/tables on every single HTTP request. This introduces significant I/O latency and database overhead per request.
**Action:** Always perform database initialization (e.g. `EnsureCreated` or `Migrate`) once during application startup in `Program.cs` inside a temporary service scope, rather than in the `DbContext` constructor.
