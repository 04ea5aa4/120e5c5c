namespace LinkPage.Links
{
    public class ClassicLink : Link
    {
        public string Url { get; set; }

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
                otherLink.Title == Title &&
                otherLink.Url == Url;
        }
    }
} 