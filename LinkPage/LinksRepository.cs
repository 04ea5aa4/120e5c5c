using LinkPage.Links.Classic;
using LinkPage.Links.Shows;

namespace LinkPage
{
    public class LinksRepository
    {
        private readonly List<ClassicLink> _links = new();

        public LinksRepository()
        {
            PopulateTestData();
        }

        public LinksRepository(IEnumerable<ClassicLink> data)
        {
            _links = data.ToList();
        }

        public ClassicLink Add(int userId, ClassicLink link)
        {
            var newLink = link.Clone();
            newLink.LinkId = _links.Count + 1;
            newLink.UserId = userId;

            _links.Add(newLink);

            return newLink.Clone();
        }

        public T? GetLink<T>(int userId, int linkId) => _links
            .Where(link => link.UserId == userId && link.LinkId == linkId)
            .OfType<T>()
            .FirstOrDefault();

        public IEnumerable<ClassicLink> GetLinks(int userId) => _links
            .Where(link => link.UserId == userId);

        private void PopulateTestData()
        {
            Add(1, new ShowsLink
            {
                LinkId = 1,
                UserId = 1,
                Title = "La Bohème",
                Shows = new List<ShowsLink.Show>
                {
                    new ShowsLink.Show
                    {
                        Id = 1,
                        Date = new DateTime(2022, 1, 4),
                        VenueName = "Sydney Opera House",
                        VenueLocation = "Sydney, Australia",
                        IsSoldOut = true,
                        IsOnSale = false,
                    },
                    new ShowsLink.Show
                    {
                        Id = 2,
                        Date = new DateTime(2022, 1, 1),
                        VenueName = "The Aquarium of Western Australia",
                        VenueLocation = "Perth, Australia",
                        IsSoldOut = false,
                        IsOnSale = false,
                    },
                    new ShowsLink.Show
                    {
                        Id = 2,
                        Date = new DateTime(2022, 1, 12),
                        VenueName = "Chinese Garden of Friendship",
                        VenueLocation = "Sydney, Australia",
                        IsSoldOut = false,
                        IsOnSale = true,
                    },
                    new ShowsLink.Show
                    {
                        Id = 2,
                        Date = new DateTime(2022, 1, 1),
                        VenueName = "Centre Ivanhoe",
                        VenueLocation = "Melbourne, Australia",
                        IsSoldOut = false,
                        IsOnSale = true,
                    },
                },
            });

            Add(1, new ClassicLink
            {
                LinkId = 2,
                UserId = 1,
                Title = "DuckDuckGo",
                Url = "https://duckduckgo.com",
            });

            Add(1, new ClassicLink
            {
                LinkId = 3,
                UserId = 1,
                Title = "Signal",
                Url = "https://signal.org",
            });
        }
    }
}
