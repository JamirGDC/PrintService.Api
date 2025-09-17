using PrintService.Application.DTOs.Events;
using PrintService.Application.Interfaces.Services;

namespace PrintService.Infraestructure.SignalR;

public class SignalRNotificationService : INotificationService
{
    private readonly INotificationHubContext _hubContext;

    public SignalRNotificationService(INotificationHubContext hubContext)
    {
        _hubContext = hubContext;
    }

    public Task NotifyJobCreated(string region, Guid jobId) =>
        _hubContext.SendToGroup(HubGroups.Region(region), "JobCreated", new JobCreatedEvent(jobId));

    public Task NotifyGetJob(string clientId, Guid jobId) =>
        _hubContext.SendToGroup(HubGroups.Agent(clientId), "JobClaimed", new JobClaimedEvent(jobId));

    public Task NotifyJobFinished(string userId, Guid jobId) =>
        _hubContext.SendToGroup(HubGroups.User(userId), "JobFinished", new JobFinishedEvent(jobId, "Finished", DateTime.UtcNow));
}
