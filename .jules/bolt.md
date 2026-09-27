## 2025-02-18 - EF Core Dynamic Property Queries via EF.Property

**Learning:** Manually constructing `System.Linq.Expressions.Expression` tree nodes at runtime for dynamic property filtering (`Expression.Parameter`, `Expression.Property`, `Expression.Lambda`) incurs expression building allocation overhead and bypasses simple C# static expression compilation. Using `EF.Property<T>(entity, propertyName)` in lambda expressions (`x => Equals(EF.Property<T>(x, key), value)`) allows C# to compile static expression trees at build time while letting EF Core handle dynamic property lookup efficiently.
**Action:** Prefer `EF.Property<TProperty>(x, propertyName)` over manual `System.Linq.Expressions` reflection reflection trees when writing generic repository helper methods in EF Core.
