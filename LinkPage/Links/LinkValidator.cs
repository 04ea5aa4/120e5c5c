using FluentValidation;

namespace LinkPage.Links
{
    public class LinkValidator : AbstractValidator<Link>
    {
        public LinkValidator()
        {
            RuleFor(link => link.Title).NotEmpty();
            RuleFor(link => link.Title).MaximumLength(144);
        }
    }
}
