using Microsoft.AspNetCore.SignalR;

namespace PCShop_api.SignalR
{
    public class SignalRHub:Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine(this.Context.ConnectionId);
            return base.OnConnectedAsync();
        }
    }
}
