using PrintService.Domain.Entities;

namespace PrintService.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(Guid? id);
    Task<T> Create(T model, CancellationToken cancellationToken);
    Task<T> Update(Guid id, T model);
    Task<T> Delete(Guid id);
    IQueryable<T> Query();
}
