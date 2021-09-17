using System.Threading.Tasks;

namespace Web.Core.Contracts
{
    public interface ISpeechRecognizer
    {
        Task<string> Recognize(string filePath);
    }
}