## 2025-05-18 - Avoid Database.EnsureCreated() in DbContext Constructor
**Learning:** Invoking `Database.EnsureCreated()` inside the constructor of a scoped `DbContext` executes schema/table existence verification queries on every single HTTP request resolving the context.
**Action:** Move `Database.EnsureCreated()` (or EF migrations) to a single startup initialization block in `Program.cs` via `app.Services.CreateScope()`, eliminating database schema check overhead on per-request database context instantiations.

## 2025-05-18 - Index Customer.Email for Fast Uniqueness Checks
**Learning:** `CustomerService.IsExists("Email", ...)` and `IsExistsForUpdate(id, "Email", ...)` validate uniqueness on customer creates and updates using dynamic expressions (`AnyAsync(x => x.Email == value)`). Without an explicit index in SQLite / EF Core, every create/update triggers a full table scan (`SCAN Customers`, O(N)).
**Action:** Define a unique index on `Customer.Email` via `modelBuilder.Entity<Customer>().HasIndex(c => c.Email).IsUnique()` in EF Core configurations and the SQLite database schema (`IX_Customers_Email`). SQLite switches from `SCAN Customers` to `SEARCH Customers USING COVERING INDEX IX_Customers_Email (Email=?)` (O(log N)), cutting query latency and disk I/O on customer mutations.
