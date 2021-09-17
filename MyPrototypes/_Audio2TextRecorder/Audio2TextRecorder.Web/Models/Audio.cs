namespace Audio2TextRecorder.Web.Models
{
    public class Audio : EntityBase
    {
        public string Title { get; set; }
        public string AudioPath { get; set; }
        public string Transcription { get; set; }
    }
}
