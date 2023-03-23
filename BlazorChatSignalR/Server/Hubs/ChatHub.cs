using Microsoft.AspNetCore.SignalR;

namespace BlazorChatSignalR.Server.Hubs
{
    public class ChatHub:Hub
    {
        private static Dictionary<string, string> Benutzer = new Dictionary<string, string>();
        public override async Task OnConnectedAsync()
        {
            string benutzername = Context.GetHttpContext().Request.Query["benutzername"];
            Benutzer.Add(Context.ConnectionId, benutzername);
            await SendeNachricht(string.Empty, $"{benutzername} ist verbunden :)");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string benutzername = Benutzer.FirstOrDefault(b => b.Key == Context.ConnectionId).Value;
            await SendeNachricht(string.Empty, $"{benutzername} ist auf der linken Seite");
        }
        public async Task SendeNachricht(string benutzer, string nachricht)
        {
            await Clients.All.SendAsync("MeineErsteNachricht", benutzer, nachricht);
        }
    }
}
