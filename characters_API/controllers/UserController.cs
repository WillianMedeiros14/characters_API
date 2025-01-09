using Microsoft.AspNetCore.Mvc;
using characters_API.Data.Dtos;
using characters_API.Services;
using characters_API.Models;

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
            await _userService.SignUp(dto);
            return Ok("Usuário cadastrado!");

        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUserDto dto)
        {
            var token = await _userService.Login(dto);
            return Ok(token);
        }
    }
}