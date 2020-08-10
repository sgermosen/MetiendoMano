using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Web.Core.Contracts;
using Web.Core.Data;
using WebPage.Helpers.Extensions;

namespace WebPage.Services
{
    public class SpeechRecognition : ISpeechRecognition
    {
        private readonly AzureCognitiveService _azureCognitiveService;
        private readonly IBMWatsonServices _watsonServices;
        private readonly IConfiguration _configuration;

        public SpeechRecognition(AzureCognitiveService azureCognitiveService,
            IBMWatsonServices watsonServices,
            IConfiguration configuration)
        {
            Guard.Against.Null(configuration, nameof(configuration));

            _configuration = configuration;
            _azureCognitiveService = azureCognitiveService;
            _watsonServices = watsonServices;
        }

        public string Transcription { get; private set; }

        public async Task<string> Recognize(string filePath, bool useAzure = false)
        {
            var operationsTimeOut = _configuration.GetValue<int>("OperationsTimeOut");

            try
            {
                if (useAzure)
                {
                    Transcription = await _azureCognitiveService.Recognize(filePath)
                        .TimeoutAfter(operationsTimeOut);
                }
                else
                {
                    Transcription = await _watsonServices.Recognize(filePath)
                        .TimeoutAfter(operationsTimeOut);
                }
            }
            catch (TimeoutException)
            {
                if (!useAzure)
                    await Recognize(filePath, true);
            }
            catch (Exception ex)
            {
                await File.WriteAllTextAsync("transcription_error_log", ex.Message);
            }

            return !string.IsNullOrEmpty(Transcription) ?
                Transcription :
                Resources.UnableToTranscriptAudio;
        }
    }
}
