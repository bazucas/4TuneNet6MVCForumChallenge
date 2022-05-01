using AutoMapper;
using Forum.Core.Models;
using Forum.Web.ViewModels;

namespace Forum.Web.Helpers;

/// <summary>
/// Automapper MappingProfiles class, inherits from <see cref="Profile"/>
/// </summary>
/// <seealso cref="AutoMapper.Profile" />
public class MappingProfiles : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfiles"/> class.
    /// </summary>
    public MappingProfiles()
    {
        CreateMap<Topic, TopicVm>()
            .ForMember(d => d.AppUserId, o => o.MapFrom(s => s.ApplicationUserId))
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.ApplicationUser!.Name));
    }
}
