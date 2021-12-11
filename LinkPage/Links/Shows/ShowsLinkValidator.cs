using FluentValidation;
using LinkPage.Links.Classic;

namespace LinkPage.Links.Shows
{
    public class ShowsLinkValidator : AbstractValidator<ShowsLink>
    {
        public ShowsLinkValidator()
        {
            Include(new ClassicLinkValidator());

            RuleFor(showsLink => showsLink.Shows)
                .Must(shows => shows.Select(show => show.Id).Distinct().Count() == shows.Count())
                .WithMessage("Show IDs must be unique within a ShowsLink");

            RuleForEach(showsLink => showsLink.Shows)
                .ChildRules(shows =>
                {
                    shows
                        .RuleFor(show => show.VenueName)
                        .NotEmpty()
                        .MaximumLength(1000);
                    shows
                        .RuleFor(show => show.VenueLocation)
                        .NotEmpty()
                        .MaximumLength(1000);
                });
        }
    }
}
