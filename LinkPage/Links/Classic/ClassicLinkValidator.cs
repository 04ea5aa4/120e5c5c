using FluentValidation;

namespace LinkPage.Links.Classic
{
    public class ClassicLinkValidator : AbstractValidator<ClassicLink>
    {
        public ClassicLinkValidator()
        {
            RuleFor(link => link.Title).NotEmpty();
            RuleFor(link => link.Title).MaximumLength(144);
            RuleFor(link => link.Url)
                .NotEmpty()
                .MaximumLength(2084); // https://stackoverflow.com/a/33733386
        }
    }
}
