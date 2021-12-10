namespace LinkPage.Links.Shows
{
    public class ShowsLink : Link
    {
        public IEnumerable<Show> Shows { get; set; } = new List<Show>();

        public override ShowsLink Clone() => new()
        {
            LinkId = LinkId,
            UserId = UserId,
            Title = Title,
            Shows = Shows.Select(show => new Show
            {
                Id = show.Id,
                Date = show.Date,
                VenueName = show.VenueName,
                VenueLocation = show.VenueLocation,
                IsSoldOut = show.IsSoldOut,
                IsOnSale = show.IsOnSale,
            }),
        };

        public override bool Equals(object? obj)
        {
            if (obj is not ShowsLink)
            {
                return false;
            }

            return obj is ShowsLink otherLink &&
                otherLink.LinkId == LinkId &&
                otherLink.UserId == UserId &&
                otherLink.Title == Title &&
                otherLink.Shows == otherLink.Shows;
        }

        public override int GetHashCode() => LinkId + UserId;

        public class Show
        {
            public int Id { get; set; }

            public DateTime Date { get; set; }

            public string VenueName { get; set; } = string.Empty;

            public string VenueLocation { get; set; } = string.Empty;

            public bool IsSoldOut { get; set; }

            public bool IsOnSale { get; set; }

            public override bool Equals(object? obj)
            {
                if (obj is not Show)
                {
                    return false;
                }

                return obj is Show otherLink &&
                    otherLink.Id == Id &&
                    otherLink.Date == Date &&
                    otherLink.VenueName == VenueName &&
                    otherLink.VenueLocation == VenueLocation &&
                    otherLink.IsSoldOut == IsSoldOut &&
                    otherLink.IsOnSale == IsOnSale;
            }

            public override int GetHashCode() =>
                Id ^ Date.GetHashCode() ^ VenueName.GetHashCode() ^
                    VenueLocation.GetHashCode() ^ IsSoldOut.GetHashCode() ^
                    IsOnSale.GetHashCode();
        }
    }
}
