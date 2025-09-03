using System.Net;
using System.Runtime.CompilerServices;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces;
using PrintService.Application.Interfaces.IServices;
using PrintService.Domain.Common.Result;
using System.Threading;
using PrintService.Application.Utilities;
using PrintService.Domain.Enums;

namespace PrintService.Application.Services;

public class JobService : IJobService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notifier;

    public JobService(IUnitOfWork unitOfWork, INotificationService notifier)
    {
        _unitOfWork = unitOfWork;
        _notifier = notifier;
    }

    public async Task<Result<CreateJobResponseDto>> CreateJobAsync(CreateJobRequestDto createJobRequest, CancellationToken cancellationToken)
    {
        var newJobPrint = await _unitOfWork.PrintJobRepository.Create(createJobRequest.ToDomain(), cancellationToken);

        if (newJobPrint == null)
            return Result<CreateJobResponseDto>.Failure(HttpStatusCode.BadRequest).WithErrors("An Error ocurred while create a product");

        await _unitOfWork.Complete(cancellationToken);

        await _notifier.NotifyJobCreated("DEU", newJobPrint.Id);

        return Result<CreateJobResponseDto>.Success(HttpStatusCode.Created).WithPayload(newJobPrint.ToResponse());
    }


    public async Task<Result<AcknowledgeJobResponseDto>> AcknowledgeJobAsync(Guid id, AcknowledgeJobRequestDto createJobRequest, CancellationToken cancellationToken)
    {
        var jobInDb = await _unitOfWork.PrintJobRepository.GetById(id);

        if (jobInDb == null)
            return Result<AcknowledgeJobResponseDto>.Failure(HttpStatusCode.BadRequest).WithErrors("Job Not Found");

        jobInDb.Status = 1;

        await _unitOfWork.PrintJobRepository.Update(jobInDb.Id, jobInDb);

        await _notifier.NotifyJobFinished(jobInDb.UserId, jobInDb.Id);

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
        var jobInDb = await _unitOfWork.PrintJobRepository.GetById(id);

        if(jobInDb == null)
            return Result<ClaimJobResponseDto>.Failure(HttpStatusCode.NotFound).WithErrors("Job Not Found");

        if (jobInDb.Status != 0)
            return Result<ClaimJobResponseDto>.Failure(HttpStatusCode.Conflict).WithErrors("Job already claimed or finished");

        jobInDb.Status = (int)JobStatus.Claimed;

        return Result<ClaimJobResponseDto>.Success(HttpStatusCode.OK);
    }
}