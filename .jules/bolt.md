## 2025-02-18 - Database Indexing and EF Core AnyAsync Mechanics
**Learning:** Adding an index on `Customer.Email` speeds up `IsExists` lookups during customer creation/updates from O(N) table scan to O(log N) index seek.
**Action:** Always verify database indexes on entity properties used for frequent existence checks or filtering.
