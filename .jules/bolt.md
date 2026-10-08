# Bolt's Journal

## 2025-05-18 - Customer Email Database Index
**Learning:** Entity Framework Core queries for `IsExists` and `IsExistsForUpdate` on `Customer.Email` perform full table scans (O(N)) unless an explicit index is defined in EF Fluent API or data annotations.
**Action:** When repository queries search or filter by entity fields (like `Email`), ensure EF Core model configurations explicitly configure database indexes (`HasIndex`) to reduce query lookup time to O(log N).
