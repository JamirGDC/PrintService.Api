using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Domain.Entities;

namespace PrintService.Application.Utilities;

public static class PrintJobMappers
{
    public static PrintJob ToDomain(this CreateJobRequestDto createJob)
    {
        return new PrintJob
        {
            UserId = createJob.UserId,
            PrinterKey = createJob.PrinterKey,
            ContentType = createJob.ContentType,
            Payload = createJob.Payload,
            Region = "DEU",
            Status = 0
        };
    }

    public static CreateJobResponseDto ToResponse(this PrintJob printJob)
    {
        return new CreateJobResponseDto
        {
            JobId = printJob.Id,
            CreatedUtc = printJob.CreatedUtc
        };
    }
}