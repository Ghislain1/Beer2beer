## 2025-05-18 - Avoid Database.EnsureCreated() in Scoped DbContext Constructor
**Learning:** Calling `Database.EnsureCreated()` inside `DbContext` constructor causes EF Core to perform database schema checks on every scoped DI creation (every HTTP request), causing significant DB I/O overhead per request.
**Action:** Ensure `Database.EnsureCreated()` or database migrations are executed once at application startup in `Program.cs` within a temporary service scope rather than in `DbContext` constructors.
