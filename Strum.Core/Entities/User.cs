//using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Strum.Core.Entities;

//public enum Region
//{
//    Lviv,
//    Kyiv,
//    Kharkiv,
//    Odessa,
//    Dnipro,
//    Donetsk,
//    Zaporizhia,
//    IvanoFrankivsk,
//    Ternopil,
//    Uzhgorod,
//    Vinnytsia,
//    Zhytomyr,
//    Cherkasy,
//    Chernihiv,
//    Sumy,
//    Poltava,
//    Mykolaiv,
//    Rivne,
//    Lutsk,
//    Kherson,
//    Khmelnytskyi,
//    Chernivtsi,
//    Kropyvnytskyi,
//    Mariupol,
//    Simferopol,
//    Sevastopol
//}
public class User
{
	public int Id { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string Email { get; set; } 
    public string PasswordHash { get; set; }
    public string Salt { get; set; }

    public List<Comment> Comments { get; set; }
    public List<Post> Posts { get; set; }
    //public string? Description { get; set; }

    //public Region? UserRegion { get; set; }
    //public string ResetToken { get; set; }
}

