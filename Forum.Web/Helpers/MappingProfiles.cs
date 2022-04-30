using AutoMapper;
using Forum.Core.Models;
using Forum.Web.ViewModels;

namespace Forum.Web.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Topic, TopicVm>()
            .ForMember(d => d.AppUserId, o => o.MapFrom(s => s.ApplicationUserId))
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.ApplicationUser!.Name));
    }
}
