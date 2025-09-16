namespace PrintService.Application.Interfaces.Services;

public interface IRequestContext
{
    string Region { get; set; }
    string CallerId { get; set; }
    Guid IdempotencyKey { get; set; }
}