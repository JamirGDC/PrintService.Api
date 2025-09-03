namespace PrintService.Infraestructure.Context;


public class RequestContext : IRequestContext
{
    public string Region { get; set; } = default!;
    public string IdempotencyKey { get; set; } = default!;
}