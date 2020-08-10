using Ardalis.GuardClauses;
using Audio2TextRecorder.Web.Contracts;
using Audio2TextRecorder.Web.Data;
using Audio2TextRecorder.Web.Models;
using Audio2TextRecorder.Web.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Audio2TextRecorder.Web.Controllers
{
    public class AudioController : Controller
    {
        private readonly IDataRepository<Audio> _audioRepository;
        private readonly ISpeechRecognition _speechRecognition;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string _subDirectory;

        public AudioController(IDataRepository<Audio> audioRepository,
            ISpeechRecognition speechRecognition,
            IConfiguration configuration,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            Guard.Against.Null(audioRepository, nameof(audioRepository));
            Guard.Against.Null(speechRecognition, nameof(speechRecognition));
            Guard.Against.Null(configuration, nameof(configuration));
            Guard.Against.Null(unitOfWork, nameof(unitOfWork));
            Guard.Against.Null(mapper, nameof(mapper));

            _audioRepository = audioRepository;
            _speechRecognition = speechRecognition;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _subDirectory = Resources.SubDirectory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var audios = _audioRepository.Query(x => true);

            var models = _mapper.Map<IEnumerable<AudioViewModel>>(audios);

            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AudioFormCreateViewModel audio)
        {
            Guard.Against.Null(audio, nameof(audio));

            if (ValidateModelState() is UnprocessableEntityObjectResult result)
                return result;

            // save file
            var fileName = $"{Guid.NewGuid()}.wav";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _subDirectory, fileName);
            var newAudio = _mapper.Map<Audio>(audio);

            await using (var stream = System.IO.File.Create(filePath))
            {
                await audio.File.CopyToAsync(stream);
            }

            newAudio.AudioPath = fileName;
            newAudio.Transcription = Resources.TranscribingAudio;


            var repository = _unitOfWork.GetRepository<Audio>();

            await repository.AddAsync(newAudio);

            await _unitOfWork.CommitAsync();

            SaveTranscriptionWheReady(newAudio.Id, filePath);

            return Ok(new
            {
                audioId = newAudio,
                audioFile = $"./{_subDirectory}/{fileName}"
            });
        }

        [HttpPost]
        public IActionResult AudioTranscription(string transcrivedText)
        {

            return Ok();
        }

        [HttpGet]
        public IActionResult EditAudio(int audioId)
        {
            var audio = _audioRepository.Get(x => x.Id == audioId);

            var model = _mapper.Map<AudioFormUpdateViewModel>(audio);

            return View(model);
        }

        [HttpPost]
        public IActionResult EditAudio(AudioFormUpdateViewModel audio)
        {
            Guard.Against.Null(audio, nameof(audio));

            if (!ModelState.IsValid)
                return View(audio);

            var repository = _unitOfWork.GetRepository<Audio>();

            var model = repository.Get(x => x.Id == audio.Id);

            _mapper.Map(audio, model);

            _unitOfWork.Commit();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAudio(int audioId)
        {
            var repository = _unitOfWork.GetRepository<Audio>();

            var model = await repository.GetAsync(x => x.Id == audioId);

            if (model is null)
                return NotFound();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _subDirectory, model.AudioPath);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            repository.Delete(model);

            _unitOfWork.Commit();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private IActionResult ValidateModelState()
        {
            if (!ModelState.IsValid)
            {
                var keys = ModelState.Keys.ToList();
                var values = ModelState.Values.ToList();
                var dictionary = values.ToDictionary(x => keys[values.IndexOf(x)]);

                return UnprocessableEntity(new
                {
                    Error = dictionary
                });
            }

            return Ok();
        }

        private async void SaveTranscriptionWheReady(int audioId, string filePath)
        {
            // created local dependencies becase the class dependencies get disposed
            // before the transcription service returns the result.
            var debContext = new ApplicationDbContext(_configuration);
            var unitOfWork = new UnitOfWork(debContext);
            var repository = unitOfWork.GetRepository<Audio>();

            var audio = repository.Get(x => x.Id == audioId);

            audio.Transcription = await _speechRecognition.Recognize(filePath);

            await repository.UpdateAsync(audio);

            if (repository.Any(x => x.Id == audioId))
                await unitOfWork.CommitAsync();

        }
    }
}
