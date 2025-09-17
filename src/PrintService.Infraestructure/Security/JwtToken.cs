using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PrintService.Application.DTOs;
using PrintService.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PrintService.Infraestructure.Security;

public class JwtToken(IOptions<JwtOptions> options) : IJwtToken
{
    private readonly JwtOptions options = options.Value;


    public TokenResult GenerateToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(options.ExpirationMinutes);

        var token = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        var handler = new JwtSecurityTokenHandler();
        var tokenString = handler.WriteToken(token);

        return new TokenResult(tokenString, options.ExpirationMinutes * 60);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(options.Key);

        try
        {
            return tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = options.Issuer,
                ValidAudience = options.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out _);
        }
        catch
        {
            return null;
        }
    }

}