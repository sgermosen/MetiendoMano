using Ardalis.GuardClauses;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.SpeechToText.v1;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Web.Core.Contracts;

namespace WebPage.Services
{
    public class IBMWatsonServices : ISpeechRecognizer
    {
        private readonly SpeechToTextService _speechToTextService;
        private IConfiguration _configuration { get; }

        public IBMWatsonServices(IConfiguration configuration)
        {
            Guard.Against.Null(configuration, nameof(configuration));
            _configuration = configuration;

            var authenticator = new IamAuthenticator(apikey: _configuration["IBMApiKey"]);
            _speechToTextService = new SpeechToTextService(authenticator);

            _speechToTextService.SetServiceUrl(_configuration["IBMApiUrl"]);
            _speechToTextService.DisableSslVerification(true);
            _speechToTextService.WithHeader("Content-Type", "application/octet-stream");
        }


        public async Task<string> Recognize(string filePath)
        {
            var transcription = "";
            var audioData = await File.ReadAllBytesAsync(filePath);
            var response = _speechToTextService.Recognize(
                audio: audioData,
                contentType: "audio/wav",
                model: _configuration["IBMIAModel"]);

            // get the first transcription of the audio.
            // this transcriptions are ordered in orden of quality (best fist).
            var result = response.Result;
            if (result.Results.Count > 0)
            {
                foreach (var speechRecognitionResult in result.Results)
                {
                    foreach (var speechRecognitionAlternative in speechRecognitionResult.Alternatives)
                    {
                        transcription = speechRecognitionAlternative.Transcript;
                        break;
                    }

                    break;
                }
            }

            return transcription;
        }
    }
}
