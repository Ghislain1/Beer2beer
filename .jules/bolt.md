# Bolt's Journal - Critical Learnings

## 2025-05-18 - Avoid calling `Database.EnsureCreated()` in Scoped `DbContext` constructors

**Learning:** Placing `Database.EnsureCreated()` inside a `DbContext` constructor causes EF Core to inspect/verify the database schema on every HTTP request when `DbContext` is registered as Scoped. This causes unnecessary disk I/O and query latency per request.

**Action:** Perform database initialization or migrations once at application startup in `Program.cs` within an explicit service scope, rather than in the `DbContext` constructor.
