using System.Net;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces;
using PrintService.Application.Interfaces.IServices;
using PrintService.Domain.Common.Result;
using System.Threading;
using PrintService.Application.Utilities;

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

        await _notifier.NotifyJobCreated(createJobRequest.UserId, newJobPrint.Id);

        return Result<CreateJobResponseDto>.Success(HttpStatusCode.Created).WithPayload(newJobPrint.ToResponse());
    }
}