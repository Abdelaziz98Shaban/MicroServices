using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        // Mapping from Platform to PlatformReadDto
        CreateMap<Platform, PlatformReadDto>();

        // Mapping from PlatformCreateDto to Platform
        CreateMap<PlatformCreateDto, Platform>();
    }
}
