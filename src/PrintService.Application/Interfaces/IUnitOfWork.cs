namespace PrintService.Application.Interfaces;

using Microsoft.EntityFrameworkCore.Storage;
using PrintService.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task<int> Complete(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken);
}
