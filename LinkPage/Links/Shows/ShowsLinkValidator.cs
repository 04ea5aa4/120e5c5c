﻿using FluentValidation;

namespace LinkPage.Links.Shows
{
    public class ShowsLinkValidator : AbstractValidator<ShowsLink>
    {
        public ShowsLinkValidator()
        {
            Include(new LinkValidator());

            RuleFor(showsLink => showsLink.Shows)
                .Must(shows => shows.Select(show => show.Id).Distinct().Count() == shows.Count())
                .WithMessage("Show IDs must be unique within a ShowsLink");

            RuleForEach(showsLink => showsLink.Shows)
                .ChildRules(shows =>
                {
                    shows.RuleFor(show => show.VenueName).NotEmpty();
                    shows.RuleFor(show => show.VenueLocation).NotEmpty();
                });
        }
    }
}