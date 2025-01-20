using characters_API.Models;

namespace characters_API.Data.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserInfoDto User { get; set; }
    }
}
