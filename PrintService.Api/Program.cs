using PrintService.Api.Extensions;
using PrintService.Api.Hubs;
using PrintService.Infraestructure.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddApiDependencies();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});


builder.Services.AddSignalR();







var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.UseCors("AllowAll");

app.MapHub<PrintHub>("/hubs/print");



app.Run();



////using Microsoft.AspNetCore.SignalR;
//using PrintService.Api.Hubs;
//using PrintService.Api.Models;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSignalR();

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//var jobs = new Dictionary<string, PrintJob>();

//var app = builder.Build();


////test signal R
//app.MapHub<PrintHub>("/hubs/print");

////test endpoint
//app.MapGet("/api/printers", () =>
//{
//    return new[]
//    {
//        new { Id = 1, Name = "HP LaserJet", Status = "Ready" },
//        new { Id = 2, Name = "Epson L3150", Status = "Busy" }
//    };
//});

//app.MapPost("/api/test/job", async (IHubContext<PrintHub> hub) =>
//{
//    var jobId = Guid.NewGuid().ToString();
//    await hub.Clients.All.SendAsync("JobCreated", jobId);
//    return Results.Ok(new { Message = "Job enviado al Hub", JobId = jobId });
//});


//app.MapPost("/v1/jobs", async (
//    HttpRequest request,
//    IHubContext<PrintHub> hub) =>
//{
//    if (!request.Headers.TryGetValue("x-region", out var region))
//        return Results.BadRequest("Missing x-region header");

//    if (!request.Headers.TryGetValue("Idempotency-Key", out var idempKey))
//        return Results.BadRequest("Missing Idempotency-Key header");

//    if (!Guid.TryParse(idempKey, out _))
//        return Results.BadRequest("Idempotency-Key must be a valid GUID");

//    if (jobs.TryGetValue(idempKey!, out var existingJob))
//    {
//        return Results.Ok(existingJob);
//    }

//    var job = new PrintJob
//    {
//        UserId = "user-123",
//        PrinterKey = null,
//        ContentType = "text/plain",
//        Payload = "Hello World",
//        Region = region!,
//    };

//    jobs[idempKey!] = job;

//    await hub.Clients.All.SendAsync("JobCreated", job.JobId);

//    return Results.Ok(job);
//});




//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();



