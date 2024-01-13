using Strum.Core.Entities;

namespace Strum.Security;

public interface IJwtTokenGenerator
{
    string CreateToken(User user);
}