namespace PrintService.Infraestructure.Context;

public interface IRequestContext
{
    string Region { get; }
    string IdempotencyKey { get; }
}