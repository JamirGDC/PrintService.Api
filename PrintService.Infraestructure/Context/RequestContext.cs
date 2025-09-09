using PrintService.Application.Interfaces.IServices;

namespace PrintService.Infraestructure.Context;


public class RequestContext : IRequestContext
{
    public string Region { get; set; }
    //public string IdempotencyKey { get; set; } = default!;
}