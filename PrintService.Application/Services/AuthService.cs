using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrintService.Application.DTOs.Request;
using PrintService.Application.DTOs.Response;
using PrintService.Application.Interfaces.Services;
using PrintService.Domain.Common.Result;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace PrintService.Application.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<Result<TokenResponseDto>> GenerateTokenAsync(TokenRequestDto request)
    {
        var clients = _config.GetSection("AuthClients").Get<List<AuthClient>>() ?? [];
        var client = clients.FirstOrDefault(c =>
            c.ClientId == request.ClientId && c.ClientSecret == request.ClientSecret);

        if (client is null)
            return Result<TokenResponseDto>.Failure(HttpStatusCode.BadRequest).WithDescription("Invalid client credentials");

        var claims = new List<Claim>
        {
            new Claim("client_id", request.ClientId),
        };

        foreach (var scope in client.AllowedScopes)
        {
            claims.Add(new Claim("scope", scope));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(1);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        var response = new TokenResponseDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresIn = (int)(expires - DateTime.UtcNow).TotalSeconds,
            Scope = string.Join(" ", client.AllowedScopes),
        };

        return Result<TokenResponseDto>.Success(HttpStatusCode.Accepted).WithPayload(response);
    }
}


public class AuthClient
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public List<string> AllowedScopes { get; set; } = new();
}