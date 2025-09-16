using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Shared.Result;

namespace PrintService.Application.Interfaces.Services;

public interface IJobService
{
    Task <Result<CreateJobResponseDto>>CreateJobAsync(CreateJobRequestDto createJobRequest, CancellationToken cancellationToken);
    Task <Result<AcknowledgeJobResponseDto>> AcknowledgeJobAsync(Guid id, AcknowledgeJobRequestDto createJobRequest, CancellationToken cancellationToken);
    Task <Result<ClaimJobResponseDto>> ClaimJobAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<PendingJobResponseDto>>> GetPendingJobsAsync(Guid deviceId, DateTime? after, CancellationToken cancellationToken);
    Task<Result<JobDetailResponseDto>> GetJobByIdAsync(Guid id, CancellationToken cancellationToken);


}