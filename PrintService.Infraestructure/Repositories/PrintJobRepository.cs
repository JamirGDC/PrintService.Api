using PrintService.Application.Interfaces.Repositories;
using PrintService.Domain.Entities;
using PrintService.Infraestructure.Data;

namespace PrintService.Infraestructure.Repositories;

public class PrintJobRepository : GenericRepository<PrintJob>, IPrintJobRepository
{
    public PrintJobRepository(ApplicationDbContext options) : base(options)
    {
    }
}