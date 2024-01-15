using Microsoft.EntityFrameworkCore;
using Strum.Core.Enums;

namespace Strum.Core.Entities;

public class Vacancy
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Likes { get; set; }
    public DateTime DatePosted { get; set; }
    public Region? Region { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Comment> Comments { get; set; }
}