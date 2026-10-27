## 2025-05-18 - Guard Request/Response Logging Middleware Header Serialization
**Learning:** Evaluating header dictionaries with `StringBuilder` and string interpolations in ASP.NET Core middleware without `_logger.IsEnabled(LogLevel.Information)` guards causes heavy GC heap allocations on every single HTTP request even when logging is disabled or filtered.
**Action:** Always check `_logger.IsEnabled(...)` before executing string-building logic or header dictionary iteration in HTTP middleware and use structured logging message templates.

## 2025-05-18 - Avoid Database.EnsureCreated() in DbContext Constructor
**Learning:** Invoking `Database.EnsureCreated()` inside the constructor of a scoped `DbContext` executes schema/table existence verification queries on every single HTTP request resolving the context.
**Action:** Move `Database.EnsureCreated()` (or EF migrations) to a single startup initialization block in `Program.cs` via `app.Services.CreateScope()`, eliminating database schema check overhead on per-request database context instantiations.
