using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintService.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    AgentRegion = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PrintersJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastSeenUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiresUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OccurredUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Attempts = table.Column<int>(type: "int", nullable: false),
                    NextVisibleUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrintJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PrinterKey = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ActualPrinterName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayloadHash = table.Column<byte[]>(type: "varbinary(32)", nullable: true),
                    SignatureKeyId = table.Column<int>(type: "int", nullable: false),
                    Signature = table.Column<byte[]>(type: "varbinary(64)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Attempts = table.Column<int>(type: "int", nullable: false),
                    ClaimedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ErrorCode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiresUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SigningHmacKeys",
                columns: table => new
                {
                    OwnerCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    KeyId = table.Column<int>(type: "int", nullable: false),
                    Secret = table.Column<byte[]>(type: "varbinary(64)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SigningHmacKeys", x => new { x.OwnerCode, x.KeyId });
                });

            migrationBuilder.CreateTable(
                name: "UserPrinters",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PrinterKey = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ChangedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPrinters", x => new { x.UserId, x.PrinterKey });
                });

            migrationBuilder.CreateTable(
                name: "IdempotencyKeys",
                columns: table => new
                {
                    CallerId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdempotencyKeys", x => new { x.CallerId, x.Key });
                    table.ForeignKey(
                        name: "FK_IdempotencyKeys_PrintJobs_JobId",
                        column: x => x.JobId,
                        principalTable: "PrintJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId_AgentRegion",
                table: "Devices",
                columns: new[] { "UserId", "AgentRegion" });

            migrationBuilder.CreateIndex(
                name: "IX_IdempotencyKeys_JobId",
                table: "IdempotencyKeys",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_ProcessedUtc_NextVisibleUtc_OccurredUtc",
                table: "OutboxMessages",
                columns: new[] { "ProcessedUtc", "NextVisibleUtc", "OccurredUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_PrintJobs_Region_Status",
                table: "PrintJobs",
                columns: new[] { "Region", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_PrintJobs_Status_CreatedUtc",
                table: "PrintJobs",
                columns: new[] { "Status", "CreatedUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_PrintJobs_UserId_Status",
                table: "PrintJobs",
                columns: new[] { "UserId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_UserPrinters_UserId_IsDefault",
                table: "UserPrinters",
                columns: new[] { "UserId", "IsDefault" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "IdempotencyKeys");

            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropTable(
                name: "SigningHmacKeys");

            migrationBuilder.DropTable(
                name: "UserPrinters");

            migrationBuilder.DropTable(
                name: "PrintJobs");
        }
    }
}
