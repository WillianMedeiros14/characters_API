using AutoMapper;
using Microsoft.AspNetCore.Identity;
using characters_API.Data.Dtos;
using characters_API.Models;

namespace characters_API.Services
{
    public class UserService
    {
        private IMapper _mapper;
        private UserManager<UserModel> _userManager;
        private SignInManager<UserModel> _signInManager;
        private TokenService _tokenService;

        public UserService(IMapper mapper, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> SignUp(CreateUserDto dto)
        {
            UserModel user = _mapper.Map<UserModel>(dto);

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário!");
            }

            return await Login(new LoginUserDto { Email = dto.Email, Password = dto.Password });

        }

        public async Task<LoginResponseDto> Login(LoginUserDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new ApplicationException("Usuário não encontrado!");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, dto.Password, false, false);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }

            var token = _tokenService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                User = new UserInfoDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                }
            };
        }

    }
}
