namespace PrintService.Infraestructure.SignalR;

public static class HubGroups
{
    public static string Agent(string clientId) => $"agent:{clientId}";
    public static string Region(string region) => $"region:{region}";
    public static string User(string userId) => $"user:{userId}";
}
