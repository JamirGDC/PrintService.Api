namespace PrintService.Application.Interfaces.Services;

public interface IConnectionManager
{
    void AddConnection(string connectionId, string clientId, string? region);
    void RemoveConnection(string connectionId);
    (string ClientId, string? Region)? GetConnection(string connectionId);
}