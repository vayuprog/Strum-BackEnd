using AutoMapper;
using Strum.Core.Entities;
using Strum.Logic.Commands;

namespace Strum_Back.Mapper;

public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<UserCreateRequest, User>();
        
    }
}