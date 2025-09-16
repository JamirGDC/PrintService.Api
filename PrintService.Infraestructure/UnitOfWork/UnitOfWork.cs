namespace PrintService.Infraestructure.UnitOfWork;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using PrintService.Application.Interfaces;
using PrintService.Application.Interfaces.Repositories;
using PrintService.Application.Interfaces.Services;
using PrintService.Domain.Entities;
using PrintService.Infraestructure.Data;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork, IDisposable, IAsyncDisposable
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<int> Complete(CancellationToken cancellationToken)
    {
        var newJobs = _dbContext.ChangeTracker.Entries<PrintJob>()
            .Where(e => e.State == EntityState.Added)
            .Select(e => e.Entity)
            .ToList();

        if (newJobs.Any())
        {
            var requestContext = _dbContext.GetService<IRequestContext>();

            if (requestContext != null && requestContext.IdempotencyKey != Guid.Empty)
            {
                foreach (var job in newJobs)
                {
                    _dbContext.IdempotencyKeys.Add(new IdempotencyKey
                    {
                        CallerId = requestContext.CallerId,
                        Key = requestContext.IdempotencyKey.ToString(),
                        JobId = job.Id,
                        CreatedUtc = DateTime.UtcNow,
                        ExpiresUtc = DateTime.UtcNow.AddHours(1)
                    });
                }
            }
        }

        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
       dbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
       await dbContext.DisposeAsync();
    }

    public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken) => await dbContext.Database.BeginTransactionAsync(cancellationToken);

    
}
