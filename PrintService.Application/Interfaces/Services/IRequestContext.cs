namespace PrintService.Application.Interfaces.Services;

public interface IRequestContext
{
    string Region { get; set; }
    //string? IdempotencyKey { get; }
}