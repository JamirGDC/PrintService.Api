namespace PrintService.Application.Interfaces;

using Microsoft.EntityFrameworkCore.Storage;

public interface IUnitOfWork
{
    Task<int> Complete(CancellationToken cancellationToken);
    void Dispose();
    Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken);
}
