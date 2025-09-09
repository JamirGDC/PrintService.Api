using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces.Services;
using PrintService.Domain.Common.Result;

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
    public async Task<Result<CreateJobResponseDto>> CreateJob([FromBody] CreateJobRequestDto createJobRequest, CancellationToken cancellationToken)
    {
        return await _jobService.CreateJobAsync(createJobRequest, cancellationToken);
    }

    [HttpPost("{id:guid}/acknowlodeg")]
    public async Task<Result<AcknowledgeJobResponseDto>> AcknowledgeJob(Guid id, [FromBody] AcknowledgeJobRequestDto acknowledgeJobRequest, CancellationToken cancellationToken)
    {
        return await _jobService.AcknowledgeJobAsync(id, acknowledgeJobRequest, cancellationToken);
    }

    [HttpPost("{id:guid}/claim")]
    public async Task<Result<ClaimJobResponseDto>> ClaimJob(Guid id, CancellationToken cancellationToken)
    {
        return await _jobService.ClaimJobAsync(id, cancellationToken);
    }
}