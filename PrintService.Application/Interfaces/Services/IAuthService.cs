using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Shared.Result;

namespace PrintService.Application.Interfaces.Services;

public interface IAuthService
{
    Task<Result<TokenResponseDto>> GenerateTokenAsync(TokenRequestDto request);
}