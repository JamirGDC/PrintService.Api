using Microsoft.EntityFrameworkCore;
using PrintService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrintService.Infraestructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<PrintJob> PrintJobs { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<UserPrinter> UserPrinters { get; set; }
    public DbSet<IdempotencyKey> IdempotencyKeys { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
