using TaskManagerAPI.DTO;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Services.Interface
{
    public interface IUserService
    {
        Task<GlobalResponseDto<string>> RegisterUser(RegisterUserDto user);
        Task<GlobalResponseDto<string>> UserLogin(LoginUserDto user);
        Task<GlobalResponseDto<User>> GetUserById(string _);
    }
}
