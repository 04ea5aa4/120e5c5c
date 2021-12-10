namespace LinkPage.Links
{
    public class ShowsLink : Link
    {
        public IEnumerable<Show> Shows { get; set; }

        public override ShowsLink Clone() => new()
        {
            Id = Id,
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

            public string VenueName { get; set; }

            public string VenueLocation { get; set; }

            public bool IsSoldOut { get; set; }

            public bool IsOnSale { get; set; }
        }
    }
}
