using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Domain.Common.Result;

namespace PrintService.Application.Interfaces.Services;

public interface IAuthService
{
    Task<Result<TokenResponseDto>> GenerateTokenAsync(TokenRequestDto request);
}