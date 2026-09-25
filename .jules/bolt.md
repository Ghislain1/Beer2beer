# Bolt's Journal - Critical Learnings

## 2025-05-20 - ASP.NET Core Middleware Logging Allocations
**Learning:** Using interpolated strings (`$"..."`) in `ILogger.LogInformation` allocates heap strings on every HTTP request regardless of log filtering, and `StringBuilder` concatenation for headers creates unnecessary GC pressure compared to `string.Join` or structured templates.
**Action:** Always use structured message templates (`LogInformation("Request: {Method} {Path}", method, path)`) and optimized string formatting in high-frequency middleware paths to avoid GC allocations per request.
