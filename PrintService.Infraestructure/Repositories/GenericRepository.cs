using Microsoft.EntityFrameworkCore;
using PrintService.Application.Interfaces.Repositories;
using PrintService.Domain.Entities;
using PrintService.Infraestructure.Data;

namespace PrintService.Infraestructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected DbSet<T> _dbSet;
    protected DbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
        _dbContext = dbContext;
    }

    public async Task<T> GetById(Guid? id)
    {
        var recordDb = await _dbSet.FindAsync(id);
        return recordDb!;
    }

    public async Task<T> Create(T model, CancellationToken cancellationToken)
    {
        model.CreatedUtc = DateTime.UtcNow;
        await _dbSet.AddAsync(model, cancellationToken);
        return model;
    }

    public virtual async Task<T> Update(Guid id, T model)
    {
        var existingData = await _dbSet.FindAsync(id);

        if (existingData is null) throw new KeyNotFoundException("Model Not Found");

        model.UpdatedUtc = DateTime.UtcNow;

        _dbContext.Entry(existingData).CurrentValues.SetValues(model);

        return existingData;
    }

    public async Task<T> Delete(Guid id)
    {
        var recordToDelete = await GetById(id);

        if (recordToDelete is null) throw new Exception("Register Not Found");

        _dbContext.Remove(recordToDelete);

        return recordToDelete;
    }
}

