using Microsoft.AspNetCore.SignalR;
using PrintService.Application.Interfaces.IServices;
using PrintService.Infraestructure.SignalR;

namespace PrintService.Infraestructure.Services;

public class SignalRNotificationService : INotificationService
{
    private readonly IHubContext<PrintHub> _hubContext;

    public SignalRNotificationService(IHubContext<PrintHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyJobCreated(string userId, Guid jobId)
    {
        await _hubContext.Clients.Group($"user:{userId}")
            .SendAsync("JobCreated", new { JobId = jobId });
    }

    public async Task NotifyJobFinished(string userId, Guid jobId)
    {
        await _hubContext.Clients.Group($"user:{userId}")
            .SendAsync("JobFinished", new
            {
                JobId = jobId,
                Status = "Finished",
                FinishedAt = DateTime.UtcNow,
            });
    }
}