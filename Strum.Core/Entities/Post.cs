namespace Strum.Core.Entities;

public class Post
{
    public string Text { get; set; }
    public byte[]? PostImage { get; set; }
    public DateTime DatePosted { get; set; }
    
    public int Id { get; set; }
    public int Likes { get; set; }
    public string Comment { get; set; }
    public int Reposts { get; set; }
    
}