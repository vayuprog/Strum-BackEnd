using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Strum.Core.Entities;
using Strum.Logic.Commands;

namespace Strum_Back.Mapper;

public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<UserCreateRequest, User>();
        CreateMap<UserUpdateRequest, User>();
        CreateMap<UserDeleteRequest, User>();
        //CreateMap<IdentityUserToken<>, User>();
    }
}