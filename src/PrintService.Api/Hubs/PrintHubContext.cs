using Microsoft.AspNetCore.SignalR;
using PrintService.Application.Interfaces.Services;

namespace PrintService.Api.Hubs;

public class PrintHubContext : INotificationHubContext
{
    private readonly IHubContext<PrintHub> _hubContext;

    public PrintHubContext(IHubContext<PrintHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task SendToGroup(string group, string method, object payload)
    {
        return _hubContext.Clients.Group(group).SendAsync(method, payload);
    }
}
