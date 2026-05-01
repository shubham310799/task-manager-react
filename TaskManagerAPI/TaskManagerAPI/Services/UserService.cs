using Microsoft.AspNetCore.Identity;
using TaskManagerAPI.Common;
using TaskManagerAPI.DTO;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Repositories.Interface;
using TaskManagerAPI.Services.Interface;

namespace TaskManagerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserService(IUserRepository userRepo, IUnitOfWork uof, ILogger<UserService> logger, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepo;
            _unitOfWork = uof;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<GlobalResponseDto<string>> RegisterUser(RegisterUserDto user)
        {
            var res = new GlobalResponseDto<string>();
            try
            {
                var existingUser = await _userRepository.GetUserByEmail(user.Email);
                if (existingUser != null)
                {
                    res.Error = ErrorCodes.UserAlreadyExistsError;
                    return res;
                }
                var newUser = new Entities.User
                {
                    UserGuid = Guid.NewGuid(),
                    Name = user.Name,
                    Email = user.Email,
                    PasswordHash = "",
                    IsActive = true,
                    CreateDt = DateTime.UtcNow,
                    IsVerified = false
                };
                newUser.PasswordHash = _passwordHasher.HashPassword(newUser, user.Password);
                await _userRepository.AddUser(newUser);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user.");
                res.Error = ErrorCodes.SomethingWentWrong;
            }
            return res;
        }

        public async Task<GlobalResponseDto<User?>> UserLogin(LoginUserDto user)
        {
            var res = new GlobalResponseDto<User?>();
            try
            {
                var existingUser = await _userRepository.GetUserByEmail(user.Email);
                if (existingUser == null)
                {
                    res.Error = ErrorCodes.UserNotFoundError;
                    return res;
                }
                var providedPasswordHash = _passwordHasher.HashPassword(existingUser, user.Password);
                var validationResult = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.PasswordHash, providedPasswordHash);
                if(validationResult == PasswordVerificationResult.Success)
                {
                    res.Data = existingUser;
                    return res;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user.");
                throw;
            }
            return res;
        }
    }
}
