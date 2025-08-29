using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces.IServices;
using PrintService.Application.Services;
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
    public async Task<Result<CreateJobResponseDto>> CreateJob([FromBody] CreateJobRequestDto createJobRequest, CancellationToken cancellationToken)
    {
        return await _jobService.CreateJobAsync(createJobRequest, cancellationToken);
    }
}