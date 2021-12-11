using FluentValidation;

namespace LinkPage.Links.Classic
{
    public class ClassicLinkValidator : AbstractValidator<ClassicLink>
    {
        public ClassicLinkValidator()
        {
            Include(new LinkValidator());
            RuleFor(link => link.Url)
                .NotEmpty()
                .MaximumLength(2084); // https://stackoverflow.com/a/33733386
        }
    }
}
