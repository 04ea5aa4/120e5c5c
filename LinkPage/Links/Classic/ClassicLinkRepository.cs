namespace LinkPage.Links
{
    public class ClassicLinkRepository
    {
        private readonly List<ClassicLinkModel> _data = new();

        public ClassicLinkRepository(IEnumerable<ClassicLinkModel> data)
        {
            _data = data.ToList();
        }

        public void Add(ClassicLinkModel model)
        {
            var newModel = model.Clone();
            newModel.Id = _data.Count + 1;
            
            _data.Add(newModel);
        }

        public ClassicLinkModel? Get(int id) => _data.Where(d => d.Id == id).FirstOrDefault();

        public IEnumerable<ClassicLinkModel> Get() => _data;
    }
}
