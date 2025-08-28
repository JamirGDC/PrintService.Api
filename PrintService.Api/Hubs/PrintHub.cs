using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace PrintService.Api.Hubs;

public class PrintHub : Hub
{
    public async Task RegisterAgent(string agentRegion, string deviceId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"region:{agentRegion}");
        await Groups.AddToGroupAsync(Context.ConnectionId, $"device:{deviceId}");

        await Clients.Caller.SendAsync("AgentRegistered", $"Agente {deviceId} en región {agentRegion} registrado");
    }

    public async Task SendPrinters(string[] printers)
    {
        await Clients.Caller.SendAsync("PrintersUpdated", printers);
    }

    public async Task NotifyJobCreated(string userId, string jobId)
    {
        await Clients.Group($"user:{userId}").SendAsync("JobCreated", jobId);
    }

}