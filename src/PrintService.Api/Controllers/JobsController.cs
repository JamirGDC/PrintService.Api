namespace PrintService.Api.Controllers;

using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security;
using Shared.Result;
using Filters;

[ApiController]
[Route("v1/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService JobService)
    {
        _jobService = JobService;
    }

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.PrintJobsWrite)]

    [RequireRegionAndIdempotency]
    [Idempotent]
    public async Task<Result<CreateJobResponseDto>> CreateJob([FromHeader(Name = "x-region")] string region, [FromHeader(Name = "x-idempotency-key")] Guid idempotencyKey, [FromBody] CreateJobRequestDto createJobRequest, CancellationToken cancellationToken)
    {
        return await _jobService.CreateJobAsync(createJobRequest, cancellationToken);
    }

    [HttpPost("{id:guid}/ack")]
    [Authorize(Policy = AuthorizationPolicies.PrintJobsAck)]

    public async Task<Result<AcknowledgeJobResponseDto>> AcknowledgeJob([FromRoute] Guid id, [FromBody] AcknowledgeJobRequestDto acknowledgeJobRequest, CancellationToken cancellationToken)
    {
        return await _jobService.AcknowledgeJobAsync(id, acknowledgeJobRequest, cancellationToken);
    }

    [HttpPost("{id:guid}/claim")]
    [Authorize(Policy = AuthorizationPolicies.PrintJobsRead)]

    public async Task<Result<ClaimJobResponseDto>> ClaimJob(Guid id, CancellationToken cancellationToken)
    {
        return await _jobService.ClaimJobAsync(id, cancellationToken);
    }

    [HttpGet("pending")]
    [Authorize(Policy = AuthorizationPolicies.PrintJobsRead)]

    public async Task<Result<IEnumerable<PendingJobResponseDto>>> GetPendingJobs([FromQuery] Guid deviceId, [FromQuery] DateTime? after, CancellationToken cancellationToken)
    {
        return await _jobService.GetPendingJobsAsync(deviceId, after, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = AuthorizationPolicies.PrintJobsRead)]

    public async Task<Result<JobDetailResponseDto>> GetJobById(Guid id, CancellationToken cancellationToken)
    {
        return await _jobService.GetJobByIdAsync(id, cancellationToken);
    }

}