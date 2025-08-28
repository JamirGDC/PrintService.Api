using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:5000/hubs/print")
    .Build();

connection.On<string>("AgentRegistered", msg =>
{
    Console.WriteLine($"[Hub] {msg}");
});

connection.On<string[]>("PrintersUpdated", printers =>
{
    Console.WriteLine("[Hub] Impresoras: " + string.Join(", ", printers));
});

connection.On<string>("JobCreated", jobId =>
{
    Console.WriteLine($"[Hub] Nuevo Job creado: {jobId}");
});


connection.On<string>("JobCreated", jobId =>
{
    Console.WriteLine($"[Hub] Nuevo Job creado: {jobId}");
});


await connection.StartAsync();

Console.WriteLine("Conectado al hub!");
await connection.InvokeAsync("RegisterAgent", "us-east", "device-123");

await connection.InvokeAsync("SendPrinters", new[] { "HP LaserJet", "Canon G3100" });

Console.ReadLine();
