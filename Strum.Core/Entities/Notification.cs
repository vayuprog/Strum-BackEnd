using Microsoft.EntityFrameworkCore;

namespace Strum.Core.Entities;


public class Notification
{
    public int Id { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
}