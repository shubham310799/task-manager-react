using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.DTO;
using TaskManagerAPI.Services.Interface;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto user)
        {
            var res = new GlobalResponseDto<string>();
            try
            {
                return Ok(await _userService.RegisterUser(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto user)
        {
            var res = new GlobalResponseDto<string>();
            try
            {
                return Ok(await _userService.UserLogin(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
