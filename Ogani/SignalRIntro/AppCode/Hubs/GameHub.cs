using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRIntro.AppCode.Hubs
{
    public class GameHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            //Clients.Caller.SendAsync("salamDe","Xos gelmisiniz!");
            Clients.Others.SendAsync("notify");

            return base.OnConnectedAsync();
        }

        public void ChangePosition(int x, int y)
        {
            Clients.Others.SendAsync("chgPosition",x,y);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}

