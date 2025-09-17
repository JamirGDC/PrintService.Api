using PrintService.Application.Services;
using System.Security.Claims;
using PrintService.Application.DTOs;

namespace PrintService.Application.Interfaces;

public interface IJwtToken
{
    TokenResult GenerateToken(IEnumerable<Claim> claims);
    ClaimsPrincipal? ValidateToken(string token);
}
