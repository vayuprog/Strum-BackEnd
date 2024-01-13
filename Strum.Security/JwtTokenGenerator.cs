using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Strum.Core.Entities;

namespace Strum.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtIssuerOptions _jwtOptions;
    public JwtTokenGenerator(IOptions<JwtIssuerOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string CreateToken(User user)
    {
    
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, _jwtOptions.JtiGenerator()),
            new Claim(
                JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(_jwtOptions.IssuedAt).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64
            )
        };
        var jwt = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            _jwtOptions.NotBefore,
            _jwtOptions.Expiration,
            _jwtOptions.SigningCredentials
        );

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return encodedJwt;
      
    }
}