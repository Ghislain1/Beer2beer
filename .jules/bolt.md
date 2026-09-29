## 2025-02-18 - Avoid calling `Database.EnsureCreated()` in `DbContext` constructor

**Learning:** Calling `Database.EnsureCreated()` inside the constructor of a scoped `DbContext` causes database schema inspection queries to execute on every single HTTP request / service scope instantiation, adding significant I/O and query overhead per request.

**Action:** Perform database schema creation (`EnsureCreated()` or `Migrate()`) once during application startup in a temporary scope in `Program.cs` rather than in the `DbContext` constructor.
