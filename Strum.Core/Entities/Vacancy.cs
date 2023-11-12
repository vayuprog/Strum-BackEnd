using Microsoft.EntityFrameworkCore;

namespace Strum.Core.Entities;

[Keyless]
public class Vacancy
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}