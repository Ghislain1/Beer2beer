## 2025-05-18 - Avoid Manual Dynamic Expression Tree Building for Entity Property Checks
**Learning:** Constructing dynamic `System.Linq.Expressions.Expression` trees via reflection in generic repository methods (e.g. `IsExists`) incurs dynamic reflection allocation overhead on every invocation and prevents EF Core query plan caching.
**Action:** Use EF Core's native `EF.Property<TValue>(x, key)` helper inside LINQ expressions, allowing EF Core to parameterize queries and cache execution plans cleanly.

## 2025-05-18 - Avoid Database.EnsureCreated() in DbContext Constructor
**Learning:** Invoking `Database.EnsureCreated()` inside the constructor of a scoped `DbContext` executes schema/table existence verification queries on every single HTTP request resolving the context.
**Action:** Move `Database.EnsureCreated()` (or EF migrations) to a single startup initialization block in `Program.cs` via `app.Services.CreateScope()`, eliminating database schema check overhead on per-request database context instantiations.
