using Model.Shared;

namespace Core.Api.ViewModels
{
    public class PhotoCreateContainerViewModel
    {
        public FileDto File { get; set; }
        public PhotoCreateDto Model { get; set; }
    }
}
