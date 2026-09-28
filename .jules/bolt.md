## 2025-05-18 - EF Core AnyAsync & Indexing Optimization
**Learning:** Adding `.AsNoTracking()` explicitly on existence check methods (`IsExists`, `IsExistsForUpdate`) and configuring database indexes on frequently queried fields (e.g. `Customer.Email`) prevents unnecessary tracking overhead and drastically improves lookup performance for validation operations during entity creation and updates.
**Action:** When validating entity fields before mutations, ensure non-pk query fields are indexed in EF Core configuration and read queries use no-tracking.
