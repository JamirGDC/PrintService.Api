using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces;
using PrintService.Application.Interfaces.Services;
using PrintService.Application.Utilities.Mappers;
using PrintService.Domain.Entities;
using PrintService.Domain.Enums;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrintService.Application.Interfaces.Repositories;
using PrintService.Shared.Result;


namespace PrintService.Application.Services;

public class JobService(
    IUnitOfWork unitOfWork,
    INotificationService notifier,
    IRequestContext requestContext,
    IGenericRepository<PrintJob> printRepository,
    ILogger<JobService> logger)
    : IJobService
{
    public async Task<Result<CreateJobResponseDto>> CreateJobAsync(CreateJobRequestDto createJobRequest, CancellationToken cancellationToken)
    {
        logger.LogError("Test");

        var region = requestContext.Region;

        var newJobPrint = await printRepository.Create(createJobRequest.ToDomain(region), cancellationToken);

        if (newJobPrint == null)
            return Result<CreateJobResponseDto>.Failure(HttpStatusCode.BadRequest).WithErrors("An Error ocurred while create a job");

        await unitOfWork.Complete(cancellationToken);

        await notifier.NotifyJobCreated(region, newJobPrint.Id);

        return Result<CreateJobResponseDto>.Success(HttpStatusCode.Created).WithPayload(newJobPrint.ToResponse());
    }


    public async Task<Result<AcknowledgeJobResponseDto>> AcknowledgeJobAsync(Guid id, AcknowledgeJobRequestDto createJobRequest, CancellationToken cancellationToken)
    {
        var jobInDb = await printRepository.GetById(id);

        if (jobInDb == null)
            return Result<AcknowledgeJobResponseDto>.Failure(HttpStatusCode.BadRequest).WithErrors("Job Not Found");

        jobInDb.Status = 1;

        await printRepository.Update(jobInDb.Id, jobInDb);

        await notifier.NotifyJobFinished(jobInDb.UserId, jobInDb.Id);

        var response = new AcknowledgeJobResponseDto
        {
            JobId = jobInDb.Id,
            Status = "finished",
            AcknowledgedAt = jobInDb.UpdatedUtc
        };

        return Result<AcknowledgeJobResponseDto>.Success(HttpStatusCode.Accepted).WithPayload(response);
    }

    public async Task<Result<ClaimJobResponseDto>> ClaimJobAsync(Guid id, CancellationToken cancellationToken)
    {
        var jobInDb = await printRepository.GetById(id);

        if(jobInDb == null)
            return Result<ClaimJobResponseDto>.Failure(HttpStatusCode.NotFound).WithErrors("Job Not Found");

        if (jobInDb.Status != 0)
            return Result<ClaimJobResponseDto>.Failure(HttpStatusCode.Conflict).WithErrors("Job already claimed or finished");

        jobInDb.Status = (int)JobStatus.Claimed;

        return Result<ClaimJobResponseDto>.Success(HttpStatusCode.OK);
    }

    public async Task<Result<IEnumerable<PendingJobResponseDto>>> GetPendingJobsAsync(Guid deviceId, DateTime? after, CancellationToken cancellationToken)
    {
        var agentRegion = requestContext.Region;

        var query = printRepository.Query()
            .Where(j => j.Status == (int)JobStatus.Pending && j.Region == agentRegion);

        if (after.HasValue)
            query = query.Where(j => j.CreatedUtc > after.Value);

        var jobs = await query
            .Select(j => new PendingJobResponseDto
            {
                JobId = j.Id,
                CreatedUtc = j.CreatedUtc
            })
            .ToListAsync(cancellationToken);

        return Result<IEnumerable<PendingJobResponseDto>>.Success(HttpStatusCode.OK).WithPayload(jobs);
    }

    public async Task<Result<JobDetailResponseDto>> GetJobByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var job = await printRepository.GetById(id);
        if (job == null)
            return Result<JobDetailResponseDto>.Failure(HttpStatusCode.NotFound).WithErrors("Job not found");

        if (!string.Equals(job.Region, requestContext.Region, StringComparison.OrdinalIgnoreCase))
        {
            return Result<JobDetailResponseDto>.Failure(HttpStatusCode.Forbidden)
                .WithErrors("Job does not belong to your region");
        }
      
        var response = new JobDetailResponseDto
        {
            JobId = job.Id,
            ContentType = job.ContentType,
            Payload = job.Payload,
            PrinterKey = job.PrinterKey
        };

        return Result<JobDetailResponseDto>.Success(HttpStatusCode.OK).WithPayload(response);
    }


}