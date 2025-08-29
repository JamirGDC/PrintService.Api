using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Services;
using PrintService.Domain.Common.Result;

namespace PrintService.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class JobsController : ControllerBase
{
    private readonly JobService _service;

    public JobsController(JobService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<Result<CreateJobResponse>> CreateJob([FromBody] CreateJobRequest request, CancellationToken ct)
    {
        return await 
    }
}