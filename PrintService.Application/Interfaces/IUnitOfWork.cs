namespace PrintService.Application.Interfaces;

using Microsoft.EntityFrameworkCore.Storage;
using PrintService.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IPrintJobRepository PrintJobRepository { get; }
    IDeviceRepository DeviceRepository { get; }

    Task<int> Complete(CancellationToken cancellationToken);
    void Dispose();
    Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken);
}
