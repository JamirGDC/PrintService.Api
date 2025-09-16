using System.Net;

namespace PrintService.Shared.Result;

public abstract class ResultBase
{
    protected ResultBase(bool isSuccess, HttpStatusCode statusCode)
    {
        IsSuccess = isSuccess;
        HttpStatusCode = statusCode;
    }
    public bool IsSuccess { get; protected set; }
    public HttpStatusCode HttpStatusCode { get; protected set; }
    public string Description { get; protected set; } = string.Empty;
    public List<string> Errors { get; protected set; } = new List<string>();
}