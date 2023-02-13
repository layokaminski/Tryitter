using System.Text;
using Tryitter.Models;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Tryitter.Token
{
  public class TokenGenerator
  {
    public string Generate(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();

      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        Subject = AddClaims(user),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Tryitter123456789!@#")),
          SecurityAlgorithms.HmacSha256Signature
        ),
        Expires = DateTime.Now.AddDays(1)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity AddClaims(User user)
    {
      var claims = new ClaimsIdentity();

      claims.AddClaim(new Claim("Id", user.UserID.ToString()));
      claims.AddClaim(new Claim("Email", user.Email));
      claims.AddClaim(new Claim("Password", user.Password));

      return claims;
    }
  }
}