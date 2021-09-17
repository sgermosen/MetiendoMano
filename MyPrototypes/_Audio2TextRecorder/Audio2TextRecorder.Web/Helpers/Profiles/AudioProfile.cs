using Audio2TextRecorder.Web.Models;
using AutoMapper;

namespace Audio2TextRecorder.Web.Helpers.Profiles
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
