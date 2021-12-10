using LinkPage.Links;

namespace LinkPage
{
    public class LinksRepository
    {
        private readonly List<Link> _links = new();

        public LinksRepository(IEnumerable<Link> data)
        {
            _links = data.ToList();
        }

        public Link Add(int userId, Link link)
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

        public IEnumerable<Link> GetLinks(int userId) => _links
            .Where(link => link.UserId == userId);
    }
}
