using PrintService.Application.Interfaces.IRepositories;

namespace PrintService.Infraestructure.UnitOfWork;

using Microsoft.EntityFrameworkCore.Storage;
using PrintService.Application.Interfaces;
using PrintService.Infraestructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public IPrintJobRepository PrintJobRepository { get; }

    public UnitOfWork
    (

        ApplicationDbContext dbContext, IPrintJobRepository printJobRepository)
    {
        _dbContext = dbContext;
        PrintJobRepository = printJobRepository;
    }

    public async Task<int> Complete(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken)
    {
        return await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }
}
