using PrintService.Application.Interfaces.Services;

namespace PrintService.Infraestructure.SignalR;

using System.Collections.Concurrent;

public class ConnectionManager : IConnectionManager
{
    private readonly ConcurrentDictionary<string, (string ClientId, string? Region)> _connections = new();

    public void AddConnection(string connectionId, string clientId, string? region)
        => _connections[connectionId] = (clientId, region);

    public void RemoveConnection(string connectionId)
        => _connections.TryRemove(connectionId, out _);

    public (string ClientId, string? Region)? GetConnection(string connectionId)
        => _connections.TryGetValue(connectionId, out var conn) ? conn : null;
}
