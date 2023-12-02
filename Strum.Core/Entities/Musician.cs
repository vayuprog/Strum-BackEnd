namespace Strum.Core.Entities;

public class Musician
{
    public int Id { get; set; }
    public string Ganre { get; set; }
    public string Expirience { get; set; }
    public string Instrument { get; set; }
    public List<User> Users { get; set; }
}