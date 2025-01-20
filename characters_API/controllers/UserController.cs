using Microsoft.AspNetCore.Mvc;
using characters_API.Data.Dtos;
using characters_API.Services;
using characters_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace characters_API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> CreateUser
            (CreateUserDto dto)
        {
            var response = await _userService.SignUp(dto);
            return Ok(response);

        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUserDto dto)
        {
            try
            {
                var token = await _userService.Login(dto);
                return Ok(token);
            }
            catch (ApplicationException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

    }
}