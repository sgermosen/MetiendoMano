using FluentValidation;
using WebPage.ViewModels;

namespace WebPage.Helpers.Validators
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
