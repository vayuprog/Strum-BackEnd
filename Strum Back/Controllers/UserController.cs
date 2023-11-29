using Microsoft.AspNetCore.Mvc;
using Strum.Core.Entities;

namespace Strum_Back.Controllers;
[ApiController]
[Route("api/user/")]
public class UserController : ControllerBase
{
    [HttpPost("GetUserr")]
    public User GetUser()
    {
        return new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "jlhdsfkjb"
        };
    }
}