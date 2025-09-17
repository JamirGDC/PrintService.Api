namespace PrintService.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces.Services;
using Shared.Result;

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
