namespace PrintService.Application.Interfaces.IServices;

public interface IRequestContext
{
    string Region { get; set; }
    //string? IdempotencyKey { get; }
}