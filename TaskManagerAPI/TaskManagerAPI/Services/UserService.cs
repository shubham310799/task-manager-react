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
        private readonly IPasswordHasher<string> _passwordHasher;
        private readonly ITokenService _tokenService;
        public UserService(IUserRepository userRepo, IUnitOfWork uof, ILogger<UserService> logger, IPasswordHasher<string> passwordHasher, ITokenService tokenService)
        {
            _userRepository = userRepo;
            _unitOfWork = uof;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
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

                var hashedPassword = _passwordHasher.HashPassword(user.Email, user.Password);
                var newUser = new Entities.User
                {
                    UserGuid = Guid.NewGuid(),
                    Name = user.Name,
                    Email = user.Email,
                    PasswordHash = hashedPassword,
                    IsActive = true,
                    CreateDt = DateTime.UtcNow,
                    IsVerified = false
                };
                await _userRepository.AddUser(newUser);
                await _unitOfWork.SaveChangesAsync();
                res.Data = await _tokenService.GenerateToken(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user.");
                res.Error = ErrorCodes.SomethingWentWrong;
            }
            return res;
        }

        public async Task<GlobalResponseDto<string>> UserLogin(LoginUserDto user)
        {
            var res = new GlobalResponseDto<string>();
            try
            {
                var existingUser = await _userRepository.GetUserByEmail(user.Email);
                if (existingUser == null)
                {
                    res.Error = ErrorCodes.UserNotFoundError;
                    return res;
                }
                var validationResult = _passwordHasher.VerifyHashedPassword(existingUser.Email, existingUser.PasswordHash, user.Password);
                if(validationResult == PasswordVerificationResult.Success)
                {
                    res.Data = await _tokenService.GenerateToken(existingUser);
                }
                else
                {
                    res.Error = ErrorCodes.LoginError;
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
