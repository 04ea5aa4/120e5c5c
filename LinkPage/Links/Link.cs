namespace LinkPage.Links
{
    public abstract class Link
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string LinkType => GetType().Name;

        public abstract Link Clone();
    }
}
