using Microsoft.EntityFrameworkCore;

namespace Strum.Core.Entities;


public class Vacancy
{
    public int Id { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; }
}