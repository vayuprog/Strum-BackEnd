using Microsoft.EntityFrameworkCore;
using Strum.Infrastructure;

namespace Strum_Back.Services;

public class UserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }
    public async Task<string> GetResetTokenByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        //return user.ResetToken;
        return "";
    }

    public async Task<bool> UpdatePassword(string email, string newPassword)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        user.PasswordHash = newPassword; // Make sure to hash the password before storing it
        _context.Users.Update(user);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task RemoveResetToken(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        //user.ResetToken = null;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}