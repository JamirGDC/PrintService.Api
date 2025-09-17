using Microsoft.AspNetCore.SignalR;
using PrintService.Application.Interfaces.Services;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using PrintService.Infraestructure.SignalR;

namespace PrintService.Api.Hubs;

[Authorize]
public class PrintHub : Hub
{
    private readonly ILogger<PrintHub> _logger;
    private readonly IConnectionManager _connections;

    public PrintHub(ILogger<PrintHub> logger, IConnectionManager connections)
    {
        _logger = logger;
        _connections = connections;
    }

    public override async Task OnConnectedAsync()
    {
        var clientId = Context.User?.FindFirst("client_id")?.Value;
        var region = Context.GetHttpContext()?.Request.Query["region"];

        if (string.IsNullOrEmpty(clientId))
        {
            _logger.LogWarning("Connection rejected: no client_id");
            Context.Abort();
            return;
        }

        _connections.AddConnection(Context.ConnectionId, clientId, region);

        await Groups.AddToGroupAsync(Context.ConnectionId, HubGroups.Agent(clientId));

        if (!string.IsNullOrEmpty(region))
            await Groups.AddToGroupAsync(Context.ConnectionId, HubGroups.Region(region!));

        _logger.LogInformation("Client {ClientId} connected with region {Region}", clientId, region);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _connections.RemoveConnection(Context.ConnectionId);
        _logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);

        await base.OnDisconnectedAsync(exception);
    }
}
