using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Domain.Common.Result;

namespace PrintService.Application.Interfaces.IServices;

public interface IJobService
{
    Task <Result<CreateJobResponseDto>>CreateJobAsync(CreateJobRequestDto createJobRequest, CancellationToken cancellationToken);
}