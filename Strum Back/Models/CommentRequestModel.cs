namespace Strum_Back.Models
{
    public class CommentRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? PostId { get; set; }
        public int UserId { get; set; }
        public int? VacancyId { get; set; }
    }
}
