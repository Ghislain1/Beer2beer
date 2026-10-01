# Bolt's Journal - Critical Learnings

## 2025-05-18 - EF Core AsNoTracking on Generic Repository GetById Anti-Pattern
**Learning:** Replacing EF Core's `FindAsync` with `AsNoTracking().FirstOrDefaultAsync()` in generic `GetById` methods breaks change tracking on update/delete flows across the app and bypasses EF Core's first-level memory cache.
**Action:** Keep `FindAsync` or use tracked queries for general generic repository lookup methods, and restrict `AsNoTracking` strictly to explicitly read-only queries.

## 2025-05-18 - Allocation Reduction via Structured Logging in HTTP Middleware
**Learning:** String interpolation (`$"..."`) inside logger calls allocates formatted strings on every request regardless of whether the log level is enabled.
**Action:** Guard logging methods with `_logger.IsEnabled()` and use structured message templates (`LogInformation("... {Prop}", prop)`) to eliminate eager string allocations in hot middleware paths.
