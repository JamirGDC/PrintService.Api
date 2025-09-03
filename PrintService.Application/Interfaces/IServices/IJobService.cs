using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Domain.Common.Result;

namespace PrintService.Application.Interfaces.IServices;

public interface IJobService
{
    Task <Result<CreateJobResponseDto>>CreateJobAsync(CreateJobRequestDto createJobRequest, CancellationToken cancellationToken);
    Task <Result<AcknowledgeJobResponseDto>> AcknowledgeJobAsync(Guid id, AcknowledgeJobRequestDto createJobRequest, CancellationToken cancellationToken);
    Task <Result<ClaimJobResponseDto>> ClaimJobAsync(Guid id, CancellationToken cancellationToken);
}