using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces;
using PrintService.Domain.Common.Result;

namespace PrintService.Application.Services;

public class JobService : IJobService
{
    private readonly IUnitOfWork _unitOfWork;

    public JobService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateJobResponseDto>> CreateJobAsync(CreateJobRequestDto request, string? region, string? idempotencyKey, CancellationToken ct)
    {
       
    }
}