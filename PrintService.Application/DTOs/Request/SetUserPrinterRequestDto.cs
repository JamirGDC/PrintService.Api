namespace PrintService.Application.DTOs.Request;

public class SetUserPrinterRequestDto
{
    public string PrinterKey { get; set; } = string.Empty;
    public string? App { get; set; }
}