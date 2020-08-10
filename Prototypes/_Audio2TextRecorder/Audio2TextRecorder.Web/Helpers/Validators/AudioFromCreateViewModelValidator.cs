using Audio2TextRecorder.Web.Models;
using FluentValidation;

namespace Audio2TextRecorder.Web.Helpers.Validators
{
    public class AudioFromCreateViewModelValidator : AbstractValidator<AudioFormCreateViewModel>
    {
        public AudioFromCreateViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(250);
            RuleFor(x => x.File).NotNull().Must(x => x.Length > 0);
        }
    }
}
