using FluentValidation;
using WebPage.ViewModels;

namespace WebPage.Helpers.Validators
{
    public class AudioFromUpdateViewModelValidator : AbstractValidator<AudioFormUpdateViewModel>
    {
        public AudioFromUpdateViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Transcription).NotEmpty().MaximumLength(50000);
        }
    }
}
