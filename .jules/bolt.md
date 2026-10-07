# Bolt's Journal - Performance Insights

## 2025-02-18 - Avoid Database.EnsureCreated in DbContext Constructor
**Learning:** Calling `Database.EnsureCreated()` inside the `DbContext` constructor executes database existence and schema checks every time `DbContext` is instantiated (which occurs on every HTTP request in ASP.NET Core since `DbContext` is scoped).
**Action:** Always perform database initialization/migrations once during application startup in `Program.cs` within a temporary service scope instead of in the `DbContext` constructor.
