namespace PrintService.Infraestructure.UnitOfWork;

using Microsoft.EntityFrameworkCore.Storage;
using PrintService.Application.Interfaces;
using PrintService.Application.Interfaces.Repositories;
using PrintService.Infraestructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public IPrintJobRepository PrintJobRepository { get; }
    public IDeviceRepository DeviceRepository { get; }

    public UnitOfWork
    (

        ApplicationDbContext dbContext, IPrintJobRepository printJobRepository, IDeviceRepository deviceRepository)
    {
        _dbContext = dbContext;
        PrintJobRepository = printJobRepository;
        DeviceRepository = deviceRepository;
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
