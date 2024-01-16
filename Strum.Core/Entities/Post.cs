using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Strum.Core.Entities;

public class Post
{
    [Key]
    public int Id { get; set; }
    public string Text { get; set; }
    //public byte[]? PostImage { get; set; }
    public DateTime DatePosted { get; set; }
    public int Likes { get; set; }
    public int Reposts { get; set; }
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }

    public List<Comment>? Comments { get; set; }
}