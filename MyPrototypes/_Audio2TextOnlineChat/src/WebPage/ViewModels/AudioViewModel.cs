using Web.Core.Models;

namespace WebPage.ViewModels
{
    public class AudioViewModel : EntityBase
    {
        public string Title { get; set; }
        public string AudioPath { get; set; }
        public string Transcription { get; set; }
    }
}
