using Microsoft.AspNetCore.SignalR;

namespace FlexiAPI.Hubs
{
    public class ChatHub:Hub
    {

        public async Task SendMessageMainFunction(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }



    }
}
