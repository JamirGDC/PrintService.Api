namespace PrintService.Domain.Events;

public class JobCreatedEvent
{
    public Guid JobId { get; }
    public string UserId { get; }

    public JobCreatedEvent(Guid jobId, string userId)
    {
        JobId = jobId;
        UserId = userId;
    }
}