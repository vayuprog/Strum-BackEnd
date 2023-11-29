// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using Strum.Infrastructure;
// using Strum.Core.Entities;
// using System;
//
// namespace Strum_Back;
// public class UserService
// {
//     private readonly AppContext _context;
//
//     public UserService(AppContext context)
//     {
//         _context = context;
//     }
//
//     public void AddUser(string firstName, string lastName, string email, string password)
//     {
//         var newUser = new User
//         {
//             FirstName = firstName,
//             LastName = lastName,
//             Email = email,
//             PasswordHash = password // Найкраще використовувати хеш пароля
//         };
//
//         _context.Users.Add(newUser);
//         _context.SaveChanges();
//
//     }
// }