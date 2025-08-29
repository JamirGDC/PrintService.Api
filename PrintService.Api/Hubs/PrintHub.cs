using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace PrintService.Api.Hubs;

//[Authorize]
public class PrintHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var http = Context.GetHttpContext();
        var userId = Context.User?.FindFirst("sub")?.Value;
        var deviceId = http?.Request.Query["deviceId"];
        var agentRegion = http?.Request.Query["agentRegion"];

        if (!string.IsNullOrEmpty(userId))
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user:{userId}");

        if (!string.IsNullOrEmpty(deviceId))
            await Groups.AddToGroupAsync(Context.ConnectionId, $"device:{deviceId}");

        Console.WriteLine($"Hub connected: user={userId}, device={deviceId}, region={agentRegion}");

        await base.OnConnectedAsync();
    }
}