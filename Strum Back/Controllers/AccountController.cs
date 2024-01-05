using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Strum_Back.Services;

namespace Strum_Back.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly TwoFAService _twoFAService;
        private readonly UserService _userService;

        public AccountController(EmailService emailService, TwoFAService twoFAService, UserService userService)
        {
            _emailService = emailService;
            _twoFAService = twoFAService;
            _userService = userService;
        }

        [HttpPost("2fa")]
        public async Task<IActionResult> TwoFactorAuthenticate(string email)
        {
            // Generate a 2FA code
            var twoFACode = _twoFAService.Generate2FACode();

            // Send the 2FA code to the user's email
            await _emailService.SendEmailAsync(email, "Your 2FA Code", $"Your 2FA code is {twoFACode}");

            // Validate the email and code with your own logic here...
            // If the code is valid, sign in the user...

            return Ok();
        }
        
        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail(string email, string subject, string message)
        {
            await _emailService.SendEmailAsync(email, subject, message);
            return Ok();
        }
        
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            // Generate a password reset token
            var resetToken = _twoFAService.GeneratePasswordResetToken();

            // Store the reset token in your database associated with the user's email

            // Generate a password reset link
            var resetLink = _twoFAService.GeneratePasswordResetLink(email, resetToken);

            // Send the reset link to the user's email
            await _emailService.SendEmailAsync(email, "Your Password Reset Link", $"Click here to reset your password: {resetLink}");

            return Ok();
        }
        
        [HttpPost("confirm-reset-password")]
        public async Task<IActionResult> ConfirmResetPassword(string email, string resetToken, string newPassword)
        {
            // If the token is valid, reset the user's password...
            // Retrieve the stored reset token from your database using the email
            var storedResetToken = await _userService.GetResetTokenByEmail(email);

            // Compare the stored reset token with the provided resetToken
            if (storedResetToken != resetToken)
            {
                return BadRequest("Invalid reset token.");
            }

            // If the tokens match, update the user's password in the database
            var result = await _userService.UpdatePassword(email, newPassword);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating password.");
            }

            // After updating the password, remove the reset token from the database
            await _userService.RemoveResetToken(email);

            return Ok();
        }
    }
}