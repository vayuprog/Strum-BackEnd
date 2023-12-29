// ChatController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Strum_Back.Hubs;
using System.Threading.Tasks;

namespace Strum_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessageToUser(string userId, string message)
        {
            await _hubContext.Clients.User(userId).SendAsync("ReceiveMessage", User.Identity.Name, message);
            return Ok();
        }
    }
}