using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.AuthHandler.Token
{
    public class TokenService:ITokenService
    {
        public string GetToken(string SecretKey, string Audience, string Issuer, AppUser existUser, IList<string> roles)
        {
            var handler = new JwtSecurityTokenHandler();
            var privateKey = Encoding.UTF8.GetBytes(SecretKey);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, existUser.Id));
            ci.AddClaim(new Claim(ClaimTypes.Name, existUser.UserName));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, existUser.fullName));
            ci.AddClaim(new Claim(ClaimTypes.Email, existUser.Email));

            ci.AddClaims(roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList());
            //foreach (var role in roles)
            //    ci.AddClaim(new Claim(ClaimTypes.Role, role));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.MaxValue,
                Subject = ci,
                Audience = Audience,
                Issuer = Issuer,
                NotBefore = DateTime.UtcNow,
            };
            var tokenHandiling = handler.CreateToken(tokenDescriptor);
            var Token = handler.WriteToken(tokenHandiling);
            return Token;
        }
    }
}
