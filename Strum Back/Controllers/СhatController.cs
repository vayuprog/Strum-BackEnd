using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using PusherServer;
using Strum.Core.Entities;
using Strum_Back.Models;

namespace Strum.Back.Controllers
{
    [Route("api")]
    [ApiController]
    public class ChatController : Controller
    {
        [HttpPost("chat")]
        public async Task<ActionResult> Message(MessageDTO dto)
        {
            var options = new PusherOptions
            {
                Cluster = "eu",
                Encrypted = true
            };

            var pusher = new Pusher(
                "1741315",
                "b5556cee2cc853dc1dfa",
                "51227d6807b5bf5af40a",
                options);

            await pusher.TriggerAsync(
                "chat",
                "message",
                new
                {
                    username = dto.Username,
                    message = dto.Message
                });

            return Ok(new string[] { });
        }
    }
}