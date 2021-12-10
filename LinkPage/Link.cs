namespace LinkPage
{
    public class Link
    {
        public string Text { get; set; }

        public string Url { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj is not Link)
            {
                return false;
            }

            var otherLink = obj as Link;

            return otherLink != null &&
                otherLink.Text == Text &&
                otherLink.Url == Url;
        }
    }

}