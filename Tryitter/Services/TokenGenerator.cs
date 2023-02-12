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
    public string Generator(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();

      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Tryitter123456789!@#")),
          SecurityAlgorithms.HmacSha256Signature
        ),
      };

      return "";
    }
  }
}