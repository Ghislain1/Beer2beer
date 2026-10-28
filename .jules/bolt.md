## 2025-05-18 - Avoid Database.EnsureCreated() in DbContext Constructor
**Learning:** Invoking `Database.EnsureCreated()` inside the constructor of a scoped `DbContext` executes schema/table existence verification queries on every single HTTP request resolving the context.
**Action:** Move `Database.EnsureCreated()` (or EF migrations) to a single startup initialization block in `Program.cs` via `app.Services.CreateScope()`, eliminating database schema check overhead on per-request database context instantiations.

## 2025-05-19 - Cache DbSet<T> and Align Repository DI Lifetime to Scoped
**Learning:** Calling `_dbContext.Set<T>()` dynamically in every repository method causes repeated Entity Framework model metadata lookups. Furthermore, registering repositories as `Transient` when `DbContext` and services are `Scoped` creates redundant repository instances on every injection.
**Action:** Cache `_dbContext.Set<T>()` into a `readonly DbSet<T> _dbSet` field in generic repository constructors and register repositories as `Scoped` in dependency injection.
