using System.Threading.Tasks;

namespace Audio2TextRecorder.Web.Contracts
{
    public interface ISpeechRecognition
    {
        string Transcription { get; }

        Task<string> Recognize(string filePath, bool useAzure = false);
    }
}