using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Strum.Logic.Hubs;


namespace Strum_Back.Controllers
{
    
    [ApiController]
    [Route("api/ChatHub/")]
    [Authorize]
    public class ChatHubController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatHubController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("JoinRoom")]
        public async Task<IActionResult> JoinRoom(string userName)
        {
            await _hubContext.Clients.All.SendAsync("JoinRoom", userName);
            return Ok();
        }

        [HttpPost("SendMessage")]
        
        public async Task<IActionResult> SendMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync("SendMessage", message);
            return Ok();
        }
    }
}