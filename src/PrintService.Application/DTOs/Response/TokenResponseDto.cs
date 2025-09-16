namespace PrintService.Application.DTOs.Response;

public class TokenResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = "Bearer";
    public int ExpiresIn { get; set; }
    public string Scope { get; set; }
}