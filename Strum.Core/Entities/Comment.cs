namespace Strum.Core.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? PostId { get; set; }
        public Post Post { get; set; }
        public DateTime DatePosted { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

    }
}