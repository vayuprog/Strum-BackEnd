using System.ComponentModel.DataAnnotations;

namespace Strum_Back.Models
{
    public class PostRequestModel
    {
        public string Text { get; set; }
        public int UserId { get; set; }
        //[Required]
        //public IFormFile? PostImage { get; set; }
    }
}
