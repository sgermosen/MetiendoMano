using Ardalis.GuardClauses;
using Audio2TextRecorder.Web.Contracts;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Audio2TextRecorder.Web.Services
{
    // Azure cognitive service implementation information
    // https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/quickstarts/speech-to-text-from-file?tabs=linux%2Cbrowser%2Cwindowsinstall&pivots=programming-language-csharp
    public class AzureCognitiveService : ISpeechRecognizer
    {
        private readonly SpeechConfig _config;

        public AzureCognitiveService(IConfiguration configuration)
        {
            Guard.Against.Null(configuration, nameof(configuration));

            // Replace with your own subscription key and region identifier
            // from here: https://aka.ms/speech/sdkregion
            var azureSubscriptionKey = configuration["AzureApiKey"];
            var serviceRegion = configuration["AzureServiceRegion"];
            _config = SpeechConfig.FromSubscription(azureSubscriptionKey, serviceRegion);
            _config.SpeechRecognitionLanguage = configuration["AzureSpeechRecognitionLanguage"];
        }

        public async Task<string> Recognize(string filePath)
        {
            using var inputAudio = AudioConfig.FromWavFileInput(filePath);
            using var speechRecognizer = new SpeechRecognizer(_config, inputAudio);

            var result = await speechRecognizer.RecognizeOnceAsync();
            var transcription = "";

            switch (result.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    transcription = result.Text;
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(result);
                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        var erroMessage = $"CANCELED: Reason={cancellation.Reason}\n";
                        erroMessage += $"CANCELED: ErrorCode={cancellation.ErrorCode}\n";
                        erroMessage += $"CANCELED: ErrorDetails={cancellation.ErrorDetails}\n";
                        erroMessage += $"CANCELED: Did you update the subscription info?";
                        throw new OperationCanceledException(erroMessage);
                    }
                    break;
            }

            return transcription;
        }
    }
}
