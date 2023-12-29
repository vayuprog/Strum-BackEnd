// ChatHub.cs
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace Strum_Back.Hubs;
public class ChatHub : Hub
{
    public async Task SendMessageToUser(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveMessage", Context.UserIdentifier, message);
    }
}