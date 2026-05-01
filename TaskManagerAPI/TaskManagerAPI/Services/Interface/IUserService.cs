using TaskManagerAPI.DTO;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Services.Interface
{
    public interface IUserService
    {
        Task<GlobalResponseDto<string>> RegisterUser(RegisterUserDto user);
        Task<GlobalResponseDto<User?>> UserLogin(LoginUserDto user);
    }
}
