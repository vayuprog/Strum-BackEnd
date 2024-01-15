namespace Strum_Back.Models
{
    public class UserUpdateDetailsRequest
    {
        public int UserId { get; set; }
        public string? Expirience { get; set; }
        public string? Ganre { get; set; }
        public string? Instrument { get; set; }
        public string? Description { get; set; }
    }
}
