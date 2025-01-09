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

        public async Task SignUp(CreateUserDto dto)
        {
            UserModel user = _mapper.Map<UserModel>(dto);

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário!");
            }
        }

        public async Task<string> Login(LoginUserDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }

            var user = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

            var token = _tokenService.GenerateToken(user);

            return token;

        }
    }
}
