namespace LinkPage.Links
{
    public class ClassicLinkModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public ClassicLinkModel Clone() => new()
        {
            Id = Id,
            Title = Title,
            Url = Url,
        };

        public override bool Equals(object? obj)
        {
            if (obj is not ClassicLinkModel)
            {
                return false;
            }

            var otherLink = obj as ClassicLinkModel;

            return otherLink != null &&
                otherLink.Title == Title &&
                otherLink.Url == Url;
        }
    }
} 