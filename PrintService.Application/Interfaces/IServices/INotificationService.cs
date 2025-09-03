namespace PrintService.Application.Interfaces.IServices;

public interface INotificationService
{
    Task NotifyJobCreated(string region, Guid jobId);
    Task NotifyJobFinished(string userId, Guid jobId);
}