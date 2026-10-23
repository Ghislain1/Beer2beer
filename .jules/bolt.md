## 2025-05-18 - Avoid Database.EnsureCreated() in DbContext Constructor
**Learning:** Invoking `Database.EnsureCreated()` inside the constructor of a scoped `DbContext` executes schema/table existence verification queries on every single HTTP request resolving the context.
**Action:** Move `Database.EnsureCreated()` (or EF migrations) to a single startup initialization block in `Program.cs` via `app.Services.CreateScope()`, eliminating database schema check overhead on per-request database context instantiations.

## 2025-05-19 - Use EF.Property instead of manual Expression.Constant trees in BaseRepository
**Learning:** Manually constructing dynamic LINQ `Expression` trees with `Expression.Constant(value)` causes EF Core query compiler cache misses on every query with a distinct value, leading to query plan re-compilation and extra heap allocations.
**Action:** Use `EF.Property<TProperty>(x, propertyName)` in LINQ expressions so EF Core can properly parameterize query arguments and cache compiled query execution plans.
