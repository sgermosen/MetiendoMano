using Audio2TextRecorder.Web.Data;

namespace Audio2TextRecorder.Web.Models
{
    public class AudioFormUpdateViewModel : EntityBase
    {
        public string Title { get; set; }
        public string Transcription { get; set; }
        public bool IsTranscribing => Transcription == Resources.TranscribingAudio;
    }
}
