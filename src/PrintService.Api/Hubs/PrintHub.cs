using Microsoft.AspNetCore.SignalR;
using PrintService.Application.Interfaces.Services;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;

namespace PrintService.Api.Hubs;

[Authorize]
public class PrintHub : Hub
{
    private readonly ILogger<PrintHub> _logger;
    private static readonly ConcurrentDictionary<string, (string ClientId,  string? Region)> _connections
        = new();

    public PrintHub(ILogger<PrintHub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var http = Context.GetHttpContext();

        var clientId = Context.User?.FindFirst("client_id")?.Value;

        var region = http?.Request.Query["region"];

        _connections[Context.ConnectionId] = (clientId, region);

        await Groups.AddToGroupAsync(Context.ConnectionId, $"agent:{clientId}");

        if (!string.IsNullOrEmpty(region))
            await Groups.AddToGroupAsync(Context.ConnectionId, $"region:{region}");

        await base.OnConnectedAsync();
    }

}