## 2025-05-18 - Avoid Database.EnsureCreated() in Scoped DbContext Constructors
**Learning:** In ASP.NET Core Clean Architecture apps using EF Core, placing `Database.EnsureCreated()` inside the `DbContext` constructor executes schema checks on every scoped DbContext creation (every HTTP request).
**Action:** Always move `Database.EnsureCreated()` or migration checks to application startup in `Program.cs` within a service scope.
