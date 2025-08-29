namespace PrintService.Application.Interfaces;

public interface INotificationService
{
    Task NotifyJobCreated(string userId, Guid jobId);
}