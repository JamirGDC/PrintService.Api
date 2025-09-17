using System.ComponentModel;

namespace PrintService.Domain.Enums;

public enum JobStatus
{
    Pending = 0,
    Claimed = 1,
    Printed = 2,
    Failed = 3
}

public enum Scopes
{
    [Description("print.jobs.read")]
    PrintJobsRead,

    [Description("print.jobs.write")]
    PrintJobsWrite,

    [Description("print.jobs.ack")]
    PrintJobsAck
}