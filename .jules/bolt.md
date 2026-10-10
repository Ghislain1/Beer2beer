## 2025-05-10 - AsNoTracking on Repository Existence Checks
**Learning:** `AnyAsync` queries on EF Core `DbSet<T>` without `AsNoTracking()` can incur unnecessary change tracking / identity resolution setup overhead in EF Core context for read-only existence check operations (`IsExists`, `IsExistsForUpdate`).
**Action:** Always append `.AsNoTracking()` to EF Core `DbSet<T>` queries in repository methods that only evaluate conditions without returning or modifying tracked entities.
