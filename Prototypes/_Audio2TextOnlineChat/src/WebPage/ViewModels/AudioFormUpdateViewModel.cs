using Web.Core.Data;
using Web.Core.Models;

namespace WebPage.ViewModels
{
    public class AudioFormUpdateViewModel : EntityBase
    {
        public string Title { get; set; }
        public string Transcription { get; set; }
        public bool IsTranscribing => Transcription == Resources.TranscribingAudio;
    }
}
