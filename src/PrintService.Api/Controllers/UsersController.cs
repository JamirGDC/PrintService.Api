namespace PrintService.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces.Services;
using Shared.Result;

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