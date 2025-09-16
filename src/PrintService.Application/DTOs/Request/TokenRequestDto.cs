namespace PrintService.Application.DTOs.Request;

public class TokenRequestDto
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
}
