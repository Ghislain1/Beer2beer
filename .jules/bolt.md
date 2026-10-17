# Bolt's Journal

## 2025-05-18 - Avoid Database.EnsureCreated() in DbContext Constructors
**Learning:** Calling `Database.EnsureCreated()` inside a `DbContext` constructor causes EF Core to execute DB schema check queries every time a `DbContext` instance is resolved (which occurs on every HTTP request for scoped contexts in ASP.NET Core).
**Action:** Always move `Database.EnsureCreated()` or `Database.Migrate()` to host initialization at application startup (e.g. in `Program.cs`) within a service scope, keeping `DbContext` constructors lightweight and free of side-effect network/disk IO.
