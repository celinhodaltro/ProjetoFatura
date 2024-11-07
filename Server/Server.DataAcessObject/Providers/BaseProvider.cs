using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;


namespace Server.DataAcessObject.Providers;
public class BaseProvider<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseProvider(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> Get(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Insert(T entity)
    {

        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task InsertRange(IEnumerable<T> entities)
    {


        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _dbSet.Attach(entity); 
        _context.Entry(entity).State = EntityState.Modified; 
        await _context.SaveChangesAsync(); 
    }


    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }


}
