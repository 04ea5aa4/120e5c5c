namespace LinkPage.Links.Classic
{
    public class ClassicLink
    {
        public int LinkId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public string LinkType => GetType().Name;

        public virtual ClassicLink Clone() => new()
        {
            LinkId = LinkId,
            UserId = UserId,
            Title = Title,
            Url = Url,
        };

        public override bool Equals(object? obj)
        {
            if (obj is not ClassicLink)
            {
                return false;
            }

            return obj is ClassicLink otherLink &&
                otherLink.LinkId == LinkId &&
                otherLink.UserId == UserId &&
                otherLink.Title == Title &&
                otherLink.Url == Url;
        }

        public override int GetHashCode() =>
            LinkId ^ UserId ^ Title.GetHashCode() ^ Url.GetHashCode();
    }
} 