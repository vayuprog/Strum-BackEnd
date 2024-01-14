namespace Strum.Core.Entities;

public class Post
{
    public string Text { get; set; }
    public string PostImage { get; set; }
    public DateTime DatePosted { get; set; }
    
    public int Id { get; set; }
    public int Likes { get; set; }
    public int Reposts { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public List<Comment> Comments { get; set; }
}