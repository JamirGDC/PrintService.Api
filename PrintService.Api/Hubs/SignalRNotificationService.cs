using Microsoft.AspNetCore.SignalR;
using PrintService.Application.Interfaces.IServices;

namespace PrintService.Api.Hubs;

public class SignalRNotificationService : INotificationService
{
    private readonly IHubContext<PrintHub> _hubContext;

    public SignalRNotificationService(IHubContext<PrintHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyJobCreated(string region, Guid jobId)
    {
        await _hubContext.Clients.Group($"region:{region}")
            .SendAsync("JobCreated", new { JobId = jobId });
    }

    public async Task NotifyGetJob(string userId, Guid jobId)
    {
        await _hubContext.Clients.User($"agent:{userId}")
            .SendAsync("JobClaimed", new { JobId = jobId });
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