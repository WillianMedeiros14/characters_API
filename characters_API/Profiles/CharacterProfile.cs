using AutoMapper;
using characters_API.Data.Dtos;
using characters_API.Models;

namespace characters_API.Profiles;
public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<CreateCharacterDto, CharacterModel>();
        CreateMap<UpdateCharacterDto, CharacterModel>();
        CreateMap<CharacterModel, UpdateCharacterDto>();
        CreateMap<CharacterModel, ReadCharacterDto>();
    }
}