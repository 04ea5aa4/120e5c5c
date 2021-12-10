using LinkPage.Links;

namespace LinkPage
{
    public class LinksRepository
    {
        private readonly List<Link> _data = new();

        public LinksRepository(IEnumerable<Link> data)
        {
            _data = data.ToList();
        }

        public void Add(Link model)
        {
            var newModel = model.Clone();
            newModel.Id = _data.Count + 1;
            
            _data.Add(newModel);
        }

        public Link? Get(int id) => _data
            .Where(link => link.Id == id)
            .FirstOrDefault();

        public IEnumerable<Link> Get() => _data;
    }
}
