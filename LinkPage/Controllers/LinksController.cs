using Microsoft.AspNetCore.Mvc;

namespace LinkPage.Controllers
{
    [ApiController]
    [Route("v1/users/{userId}/links")]
    public class LinksController : ControllerBase
    {
        private readonly IEnumerable<Link> _links = new List<Link>()
        {
            new Link
            {
                Text = "Google",
                Url = "https://google.com",
            },
            new Link
            {
                Text = "Facebook",
                Url = "https://facebook.com",
            },
        };

        public LinksController() { }

        [HttpGet]
        public IEnumerable<Link> Get() => _links;
    }
}