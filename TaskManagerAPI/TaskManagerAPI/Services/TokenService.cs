using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Services.Interface;

namespace TaskManagerAPI.Services
{
    public class TokenService : ITokenService
    {
        public async Task<string> GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("y7K9vQ2xT8mP4sL1aZ6rD3nF5bH0uJ8wC9eX2kM4pR7tY1qW"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new System.Security.Claims.Claim("UserId", user.UserId.ToString()),
                new System.Security.Claims.Claim("Email", user.Email),
                new System.Security.Claims.Claim("Name", user.Name)
            };

            var token = new JwtSecurityToken(
                issuer: "your_app",
                audience: "your_app",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
