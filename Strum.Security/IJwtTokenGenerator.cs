namespace Strum.Security;

public interface IJwtTokenGenerator
{
    string CreateToken(string username);
}