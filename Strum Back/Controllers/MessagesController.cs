//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Strum.Core.Entities;
//using Strum.Infrastructure;

//namespace Strum_Back.Controllers;


//[ApiController]
//[Route("api/message/")]
//public class MessagesController: ControllerBase
//{
//    private readonly DataContext _context;

//    public MessagesController(DataContext context)
//    {
//        _context = context;
//    }
    
//    [HttpGet("GetMessage")]
//    public async Task<ActionResult<IEnumerable<User>>> GetMessages()
//    {
//        var messages = await _context.Messages.ToListAsync();

//        if (messages == null || messages.Count == 0)
//        {
//            return NotFound("No messages found");
//        }

//        return Ok(messages);
//    }
//    [HttpPost("AddMessage")]
//    public IActionResult AddMessage([FromBody] Messages messages)
//    {
//        try
//        {
//            _context.Messages.Add(messages);
//            _context.SaveChanges();
//            //return Ok(user);
//            return CreatedAtRoute("GetMessageById", new { id = messages.Id }, messages);
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
        
//    }
//}