using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRIntro.AppCode.Hubs
{
    public class ChatHub : Hub
    {
        static ConcurrentDictionary<string, string> users = new ConcurrentDictionary<string, string>();
        static ConcurrentDictionary<string, List<string>> groups = new ConcurrentDictionary<string, List<string>>();

        public override Task OnConnectedAsync()
        {
            //Debug.WriteLine($">>>>> {Context.ConnectionId} <<<<<");

            var ctx = Context.GetHttpContext();

            string email = ctx.Request.Query["yourKey"].ToString();

            users.TryAdd(email, Context.ConnectionId);

            Clients.Others.SendAsync("friendOnline", email);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var email = users.FirstOrDefault(u => u.Value.Equals(Context.ConnectionId)).Key;

            if (!string.IsNullOrWhiteSpace(email))
                users.TryRemove(email, out string clientId);

            Clients.Others.SendAsync("friendOffline",email);

            return base.OnDisconnectedAsync(exception);
        }

        public Task SendToFriend(string to, string message)
        {
            if (users.TryGetValue(to, out string clientId))
            {
                var email = users.FirstOrDefault(u => u.Value.Equals(Context.ConnectionId)).Key;
                Clients.Client(clientId).SendAsync("messageReceive", email, message);
            }
            return Task.CompletedTask;
        }

        public async Task<bool> SendToGroup(string groupName, string message)
        {
            var email = users.FirstOrDefault(u => u.Value.Equals(Context.ConnectionId)).Key;
            var foundE = groups[groupName]?.FirstOrDefault(uE => uE == email);
            if (foundE != null)
            {
                //await Clients.Group(groupName).SendAsync("messageReceive", email, message);
                //await Clients.GroupExcept(groupName,Context.ConnectionId).SendAsync("messageReceive", email, message);
                await Clients.OthersInGroup(groupName).SendAsync("messageReceive", email, message);
                return true;
            }
            return false;
        }

        public Task<string[]> GetOnlines()
        {
            string[] emails = users
                .Where(u => !u.Value.Equals(Context.ConnectionId))
                .Select(u => u.Key).ToArray();

            return Task.FromResult(emails);
        }

        public async Task CreateGroup(string groupName)
        {
            groups.TryAdd(groupName, new List<string>());
            await Clients.All.SendAsync("createNewGroup", groupName);
        }

        public async Task<bool> AddToGroup(string userEmail, string groupName)
        {
            var foundEmail = groups[groupName]?.FirstOrDefault(uE => uE == userEmail);

            if (users.TryGetValue(userEmail, out string clientId) && foundEmail == null)
            {
                    groups[groupName].Add(userEmail);
                    await Groups.AddToGroupAsync(clientId, groupName);
                    await Clients.All.SendAsync("friendAddedToGroup", groupName, userEmail);
                    return true;
            }

            return false;

        }

        public Task<ConcurrentDictionary<string, List<string>>> GetGroups()
        {
            return Task.FromResult(groups);
        }

    }
}

