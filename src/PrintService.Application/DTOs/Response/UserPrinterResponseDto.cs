namespace PrintService.Application.DTOs.Response;

public class UserPrinterResponseDto
{
    public string UserId { get; set; } = string.Empty;
    public string PrinterKey { get; set; } = string.Empty;
    public string? App { get; set; }
}