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

        public class Show
        {
            public int Id { get; set; }

            public DateTime Date { get; set; }

            public string VenueName { get; set; } = string.Empty;

            public string VenueLocation { get; set; } = string.Empty;

            public bool IsSoldOut { get; set; }

            public bool IsOnSale { get; set; }
        }
    }
}
