using Microsoft.AspNetCore.SignalR;
using Strum_Back.Services;
using Strum.Core.Entities;
using Strum.Logic.Utils;

namespace Strum_Back.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDictionary<string, UserConnection> _userConnections;
        private readonly UserService _userService;

        public ChatHub(IDictionary<string, UserConnection> userConnections, UserService userService)
        {
            _userConnections = userConnections;
            _userService = userService;
        }

        public async Task JoinRoom(string userName)
        {
            var now = DateTime.Now;

            _userConnections[Context.ConnectionId] = new UserConnection
            {
                Name = userName,
                JoinedAt = now
            };

            await Clients.All.SendAsync("ReceiveMessage", new
            {
                from = "System Message",
                text = $"User \"{userName}\" joined the group.",
                sentAt = now.GetEuroFormat(),
                isIncoming = true
            });

            await SendConnectedUsers();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (_userConnections.TryGetValue(Context.ConnectionId, out var userConnection))
            {
                _userConnections.Remove(Context.ConnectionId);

                Clients.All.SendAsync("ReceiveMessage", new
                {
                    from = "System Message",
                    text = $"User \"{userConnection.Name}\" left the group.",
                    sentAt = DateTime.Now.GetEuroFormat(),
                    isIncoming = true
                });

                SendConnectedUsers();
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            if (_userConnections.TryGetValue(Context.ConnectionId, out var userConnection))
            {
                var sentAt = DateTime.Now.GetEuroFormat();

                await Clients.Caller.SendAsync("ReceiveMessage", new
                {
                    from = userConnection.Name,
                    text = message,
                    sentAt,
                    isIncoming = false
                });
                await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveMessage", new
                {
                    from = userConnection.Name,
                    text = message,
                    sentAt,
                    isIncoming = true
                });
            }
        }

        public Task SendConnectedUsers()
        {
            return Clients.All.SendAsync("ReceiveConnectedUsers", _userConnections
                .Select(u => new
                {
                    name = u.Value.Name,
                    joinedAt = u.Value.JoinedAt.GetEuroFormat()
                }).OrderByDescending(u => u.joinedAt));
        }
    }
}