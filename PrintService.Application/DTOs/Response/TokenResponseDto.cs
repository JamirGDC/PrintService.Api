namespace PrintService.Application.DTOs.Response;

public class TokenResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = "Bearer";
    public int ExpiresIn { get; set; } = 3600;
    public string Scope { get; set; }
}