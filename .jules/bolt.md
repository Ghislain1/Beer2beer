## 2025-10-12 - Guard Middleware Logging Calls and Prefer Structured Logging Templates
**Learning:** In ASP.NET Core request/response middleware, string interpolation and string building (such as formatting HTTP headers) allocate objects on the heap for every request even if the target LogLevel is disabled.
**Action:** Always guard logger calls or expensive log parameter creation with `_logger.IsEnabled(LogLevel.Information)` and use structured message templates (`"Message {Param}"`) instead of interpolated strings in middleware hot paths.
