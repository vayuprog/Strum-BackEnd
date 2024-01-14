namespace Strum_Back.Models
{
    public class PostRequestModel
    {
        public string Text { get; set; }
        public int UserId { get; set; }
        public IFormFile? PostImage { get; set; }
    }
}
