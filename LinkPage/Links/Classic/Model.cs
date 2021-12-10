namespace LinkPage.Links.Classic
{
    public class Model
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj is not Model)
            {
                return false;
            }

            var otherLink = obj as Model;

            return otherLink != null &&
                otherLink.Title == Title &&
                otherLink.Url == Url;
        }
    }
} 