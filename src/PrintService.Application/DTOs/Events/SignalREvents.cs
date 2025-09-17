namespace PrintService.Application.DTOs.Events;

public record JobCreatedEvent(Guid JobId);
public record JobClaimedEvent(Guid JobId);
public record JobFinishedEvent(Guid JobId, string Status, DateTime FinishedAt);
