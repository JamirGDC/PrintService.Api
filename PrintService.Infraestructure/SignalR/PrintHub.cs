using Microsoft.AspNetCore.SignalR;

namespace PrintService.Infraestructure.SignalR;

public class PrintHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

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
}