namespace LinkPage.Links.Classic
{
    public class ClassicLink : Link
    {
        public string Url { get; set; } = string.Empty;

        public override ClassicLink Clone() => new()
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