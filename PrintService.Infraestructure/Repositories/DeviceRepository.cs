using PrintService.Application.Interfaces.IRepositories;
using PrintService.Domain.Entities;
using PrintService.Infraestructure.Data;

namespace PrintService.Infraestructure.Repositories;

public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
{
    public DeviceRepository(ApplicationDbContext options) : base(options)
    {
    }

}