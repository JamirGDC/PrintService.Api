using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces.Services;
using PrintService.Shared.Result;

namespace PrintService.Api.Controllers;

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
    [Authorize(Policy = "print.jobs.write")]
    public async Task<Result<CreateJobResponseDto>> CreateJob([FromHeader(Name = "x-region")] string region, [FromHeader(Name = "x-idempotency-key")] Guid idempotencyKey, [FromBody] CreateJobRequestDto createJobRequest, CancellationToken cancellationToken)
    {
        return await _jobService.CreateJobAsync(createJobRequest, cancellationToken);
    }

    [HttpPost("{id:guid}/ack")]
    [Authorize(Policy = "print.jobs.ack")]
    public async Task<Result<AcknowledgeJobResponseDto>> AcknowledgeJob([FromRoute] Guid id, [FromBody] AcknowledgeJobRequestDto acknowledgeJobRequest, CancellationToken cancellationToken)
    {
        return await _jobService.AcknowledgeJobAsync(id, acknowledgeJobRequest, cancellationToken);
    }

    [HttpPost("{id:guid}/claim")]
    [Authorize(Policy = "print.jobs.read")]
    public async Task<Result<ClaimJobResponseDto>> ClaimJob(Guid id, CancellationToken cancellationToken)
    {
        return await _jobService.ClaimJobAsync(id, cancellationToken);
    }

    [HttpGet("pending")]
    [Authorize(Policy = "print.jobs.read")]
    public async Task<Result<IEnumerable<PendingJobResponseDto>>> GetPendingJobs([FromQuery] Guid deviceId, [FromQuery] DateTime? after, CancellationToken cancellationToken)
    {
        return await _jobService.GetPendingJobsAsync(deviceId, after, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "print.jobs.read")]
    public async Task<Result<JobDetailResponseDto>> GetJobById(Guid id, CancellationToken cancellationToken)
    {
        return await _jobService.GetJobByIdAsync(id, cancellationToken);
    }

}