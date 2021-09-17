using System.Threading.Tasks;

namespace Audio2TextRecorder.Web.Contracts
{
    public interface ISpeechRecognizer
    {
        Task<string> Recognize(string filePath);
    }
}