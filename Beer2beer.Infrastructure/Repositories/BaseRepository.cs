

namespace Beer2beer.Infrastructure.Repositories;
using Beer2beer.Core.Exceptions;
using Beer2beer.Core.Interfaces;
using Beer2beer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//Unit of Work Pattern
public class BaseRepository<T> : IBaseRepository<T> where T : class
{

    protected readonly ApplicationDbContext _dbContext;
    protected DbSet<T> DbSet => _dbContext.Set<T>();

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        var data = await _dbContext.Set<T>()
            .AsNoTracking()
            .ToListAsync();

        return data;
    }

    //public virtual async Task<PaginatedDataViewModel<T>> GetPaginatedData(int pageNumber, int pageSize)
    //{
    //    var query = _dbContext.Set<T>()
    //        .Skip((pageNumber - 1) * pageSize)
    //        .Take(pageSize)
    //        .AsNoTracking();

    //    var data = await query.ToListAsync();
    //    var totalCount = await _dbContext.Set<T>().CountAsync();

    //    return new PaginatedDataViewModel<T>(data, totalCount);
    //}

    public async Task<T> GetById<Tid>(Tid id)
    {
        var data = await _dbContext.Set<T>().FindAsync(id);
        if (data == null)
        {
            throw new NotFoundException("No data found");
        }

        return data;
    }

    public async Task<bool> IsExists<Tvalue>(string key, Tvalue value)
    {
        // Performance optimization: EF.Property with AsNoTracking allows EF Core's Compiled Query Cache
        // to parameterize and reuse SQL execution plans, avoiding runtime Expression.Constant allocations and re-compilations.
        return await _dbContext.Set<T>()
            .AsNoTracking()
            .AnyAsync(x => EF.Property<Tvalue>(x, key)!.Equals(value));
    }

    //Before update existence check
    public async Task<bool> IsExistsForUpdate<Tid>(Tid id, string key, string value)
    {
        // Performance optimization: Parameterized query via EF.Property with AsNoTracking enables EF Core query plan caching.
        return await _dbContext.Set<T>()
            .AsNoTracking()
            .AnyAsync(x => EF.Property<string>(x, key) == value && !EF.Property<Tid>(x, "Id")!.Equals(id));
    }


    public async Task<T> Create(T model)
    {
        await _dbContext.Set<T>().AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task CreateRange(List<T> model)
    {
        await _dbContext.Set<T>().AddRangeAsync(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(T model)
    {
        _dbContext.Set<T>().Update(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(T model)
    {
        _dbContext.Set<T>().Remove(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveChangeAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

}
