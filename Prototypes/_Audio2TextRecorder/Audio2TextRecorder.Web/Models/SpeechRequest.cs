namespace Audio2TextRecorder.Web.Models
{
    public class SpeechRequest
    {
        public string MediaUrl { get; set; }
        public string Metadata { get; set; }
        public string CallbackUrl { get; set; }
    }
}
