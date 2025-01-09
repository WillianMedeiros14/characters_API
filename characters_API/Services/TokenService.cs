using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using characters_API.Models;

namespace characters_API.Services
{
    public class TokenService
    {
        public string GenerateToken(UserModel user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id.ToString())
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key.Secret));

            var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    expires: DateTime.Now.AddMinutes(10),
                    claims: claims,
                    signingCredentials: signingCredentials
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
