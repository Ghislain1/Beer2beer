## 2025-02-18 - Avoid Database.EnsureCreated() in DbContext Constructor
**Learning:** Calling `Database.EnsureCreated()` inside the DbContext constructor executes schema verification and DB I/O every time a DbContext scope is resolved (i.e., on every HTTP request), severely degrading API throughput and request latency.
**Action:** Always initialize database schema once during app startup (`Program.cs`) using an explicit `IServiceScope` rather than inside the DbContext constructor.
