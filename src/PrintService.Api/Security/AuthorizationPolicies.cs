namespace PrintService.Api.Security;

public static class AuthorizationPolicies
{
    public const string PrintJobsRead = "print.jobs.read";
    public const string PrintJobsWrite = "print.jobs.write";
    public const string PrintJobsAck = "print.jobs.ack";
}
