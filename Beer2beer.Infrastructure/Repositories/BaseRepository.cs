

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
    // Optimization: Cache DbSet<T> in a readonly field to avoid dynamic _dbContext.Set<T>() lookups on every repository operation.
    protected readonly DbSet<T> _dbSet;
    protected DbSet<T> DbSet => _dbSet;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        var data = await _dbSet
            .AsNoTracking()
            .ToListAsync();

        return data;
    }

    //public virtual async Task<PaginatedDataViewModel<T>> GetPaginatedData(int pageNumber, int pageSize)
    //{
    //    var query = _dbSet
    //        .Skip((pageNumber - 1) * pageSize)
    //        .Take(pageSize)
    //        .AsNoTracking();

    //    var data = await query.ToListAsync();
    //    var totalCount = await _dbSet.CountAsync();

    //    return new PaginatedDataViewModel<T>(data, totalCount);
    //}

    public async Task<T> GetById<Tid>(Tid id)
    {
        var data = await _dbSet.FindAsync(id);
        if (data == null)
        {
            throw new NotFoundException("No data found");
        }

        return data;
    }

    public async Task<bool> IsExists<Tvalue>(string key, Tvalue value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, key);
        var constant = Expression.Constant(value);
        var equality = Expression.Equal(property, constant);
        var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

        return await _dbSet.AnyAsync(lambda);
    }

    //Before update existence check
    public async Task<bool> IsExistsForUpdate<Tid>(Tid id, string key, string value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, key);
        var constant = Expression.Constant(value);
        var equality = Expression.Equal(property, constant);

        var idProperty = Expression.Property(parameter, "Id");
        var idEquality = Expression.NotEqual(idProperty, Expression.Constant(id));

        var combinedExpression = Expression.AndAlso(equality, idEquality);
        var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);

        return await _dbSet.AnyAsync(lambda);
    }


    public async Task<T> Create(T model)
    {
        await _dbSet.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task CreateRange(List<T> model)
    {
        await _dbSet.AddRangeAsync(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(T model)
    {
        _dbSet.Update(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(T model)
    {
        _dbSet.Remove(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveChangeAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

}
