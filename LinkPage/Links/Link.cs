using System.ComponentModel.DataAnnotations;

namespace LinkPage.Links
{
    public abstract class Link
    {
        public int LinkId { get; set; }

        public int UserId { get; set; }

        [Required]
        [MaxLength(144)]
        public string Title { get; set; } = string.Empty;

        public string LinkType => GetType().Name;

        public abstract Link Clone();
    }
}
