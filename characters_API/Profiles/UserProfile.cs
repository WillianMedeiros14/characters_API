using AutoMapper;

using characters_API.Models;

namespace characters_API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, UserModel>();
        }
    }
}
