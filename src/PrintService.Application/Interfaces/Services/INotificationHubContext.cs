namespace PrintService.Application.Interfaces.Services;

public interface INotificationHubContext
{
    Task SendToGroup(string group, string method, object payload);
}