using AutoMapper;
using Web.Core.Models;
using WebPage.ViewModels;

namespace WebPage.Helpers.Profiles
{
    public class AudioProfile : Profile
    {
        public AudioProfile()
        {
            CreateMap<Audio, AudioViewModel>();
            CreateMap<AudioViewModel, Audio>();

            CreateMap<Audio, AudioFormCreateViewModel>();
            CreateMap<AudioFormCreateViewModel, Audio>();

            CreateMap<Audio, AudioFormUpdateViewModel>();
            CreateMap<AudioFormUpdateViewModel, Audio>();
        }
    }
}
