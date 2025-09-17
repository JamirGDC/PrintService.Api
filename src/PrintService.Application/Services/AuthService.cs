using Microsoft.Extensions.Configuration;
using PrintService.Application.DTOs;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces;
using PrintService.Application.Interfaces.Services;
using PrintService.Shared.Result;
using System.Net;
using System.Security.Claims;

namespace PrintService.Application.Services;

public class AuthService(IJwtToken jwtToken, IConfiguration config) : IAuthService
{

    public async Task<Result<TokenResponseDto>> GenerateTokenAsync(TokenRequestDto request)
    {
        if (!ValidateClient(request.ClientId, request.ClientSecret, out var client))
        {
            return Result<TokenResponseDto>
                .Failure(HttpStatusCode.BadRequest)
                .WithDescription("Invalid client credentials");
        }

        var claims = new List<Claim> { new Claim("client_id", request.ClientId) };

        claims.AddRange(client!.AllowedScopes.Select(scope => new Claim("scope", scope)));

        var accessToken = jwtToken.GenerateToken(claims);

        var response = new TokenResponseDto
        {
            AccessToken = accessToken.AccessToken,
            ExpiresIn = accessToken.ExpiresIn,
            Scope = string.Join(" ", client.AllowedScopes)
        };

        return Result<TokenResponseDto>
            .Success(HttpStatusCode.Accepted)
            .WithPayload(response);
    }

    public bool ValidateClient(string clientId, string clientSecret, out AuthClient? client)
    {
        var clients = config.GetSection("AuthClients").Get<List<AuthClient>>() ?? [];
        client = clients.FirstOrDefault(c =>
            c.ClientId == clientId && c.ClientSecret == clientSecret);

        return client is not null;
    }
}

public class AuthClient
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public List<string> AllowedScopes { get; set; } = new();
}



