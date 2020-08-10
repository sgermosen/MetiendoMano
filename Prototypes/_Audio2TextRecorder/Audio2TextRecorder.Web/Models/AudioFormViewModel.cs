using Microsoft.AspNetCore.Http;

namespace Audio2TextRecorder.Web.Models
{
    public class AudioFormCreateViewModel : EntityBase
    {
        public string Title { get; set; }
        public IFormFile File { get; set; }
    }
}
