using System.Threading.Tasks;

namespace Web.Core.Contracts
{
    public interface ISpeechRecognition
    {
        string Transcription { get; }

        Task<string> Recognize(string filePath, bool useAzure = false);
    }
}