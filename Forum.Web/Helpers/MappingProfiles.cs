using AutoMapper;
using Forum.Core.Models;
using Forum.Web.ViewModels;

namespace Forum.Web.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Topic, TopicVm>();
        CreateMap<TopicVm, Topic>();
    }
}
