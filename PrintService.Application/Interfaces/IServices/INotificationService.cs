namespace PrintService.Application.Interfaces.IServices;

public interface INotificationService
{
    Task NotifyJobCreated(string userId, Guid jobId);
    Task NotifyJobFinished(string userId, Guid jobId);
}