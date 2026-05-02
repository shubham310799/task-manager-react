using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Services.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}
