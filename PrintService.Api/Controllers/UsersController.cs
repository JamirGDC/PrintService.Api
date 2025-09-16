using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces.Services;
using PrintService.Shared.Result;

namespace PrintService.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    //[HttpGet]
    //[Authorize(Policy = "print.jobs.read")]
    //public async Task<Result<UserPrinterResponseDto>> GetMyPrinter(CancellationToken cancellationToken)
    //{
    //    return await userService.GetUserPrinterAsync(cancellationToken);
    //}

    //[HttpPost]
    //[Authorize(Policy = "print.jobs.write")]
    //public async Task<Result<UserPrinterResponseDto>> SetMyPrinter([FromBody] SetUserPrinterRequestDto request, CancellationToken cancellationToken)
    //{
    //    return await userService.SetUserPrinterAsync(request, cancellationToken);
    //}
}