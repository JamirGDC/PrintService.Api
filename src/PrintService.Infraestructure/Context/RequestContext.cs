using PrintService.Application.Interfaces.Services;

namespace PrintService.Infraestructure.Context;

public class RequestContext : IRequestContext
{
    public string Region { get; set; }
    public string CallerId { get; set; }
    public Guid IdempotencyKey { get; set; }
}