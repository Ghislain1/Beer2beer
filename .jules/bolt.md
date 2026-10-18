## 2025-05-18 - Avoid calling Database.EnsureCreated in DbContext Constructor

**Learning:** Calling `Database.EnsureCreated()` inside the `DbContext` constructor causes EF Core to run schema/table existence queries against SQLite/database on every request when `DbContext` is scoped per request.

**Action:** Execute database migration/initialization once on application startup in `Program.cs` rather than inside the `DbContext` constructor.
