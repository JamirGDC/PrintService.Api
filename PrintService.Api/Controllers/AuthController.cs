using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces.IServices;
using PrintService.Domain.Common.Result;

namespace PrintService.Api.Controllers;

[ApiController]
[Route("connect")]
public class AuthController
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("token")]
    public async Task<Result<TokenResponseDto>> GetToken([FromForm] TokenRequestDto request)
    {
         return await _authService.GenerateTokenAsync(request);

    }
}
