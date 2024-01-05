using System.Security.Cryptography;

namespace Strum_Back.Services;

public class TwoFAService
{
    public string Generate2FACode()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var tokenData = new byte[4];
            rng.GetBytes(tokenData);

            var token = BitConverter.ToUInt32(tokenData, 0);
            return token.ToString("D6");
        }
    }
    public string GeneratePasswordResetToken()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var tokenData = new byte[16];
            rng.GetBytes(tokenData);

            var token = BitConverter.ToString(tokenData).Replace("-", "");
            return token;
        }
    }
    public string GeneratePasswordResetLink(string email, string resetToken)
    {
        // Replace "yourwebsite.com" with your actual website URL
        return $"https://yourwebsite.com/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(resetToken)}";
    }
}