## 2026-09-26 - Move DbContext Schema Checks Out of Per-Request Constructor

**Learning:** Calling `Database.EnsureCreated()` in the constructor of a scoped `DbContext` (like `ApplicationDbContext`) executes schema verification DDL/queries on every single HTTP request. In ASP.NET Core, scoped dependencies are instantiated per-request, leading to severe latency and database overhead under load. Moving database initialization to application startup (`Program.cs`) completely eliminates per-request schema checks.

**Action:** Always verify `DbContext` constructors do not perform synchronous or I/O operations like `EnsureCreated()` or migrations. Move database initialization tasks into an explicit application startup service or scope in `Program.cs`.
