using Microsoft.AspNetCore.Mvc;

namespace LinkPage.Links.Classic
{
    [ApiController]
    [Route("v1/users/{userId}/links/classic")]
    public class ClassicLinkController : ControllerBase
    {
        private readonly LinksRepository _repository;

        public ClassicLinkController(LinksRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Link>> Get(int userId)
        {
            var links = _repository
                .GetLinks(userId)
                .OfType<ClassicLink>();

            if (links.Any())
            {
                return Ok(links.Select(link => (object)link));
            }

            return NotFound(Array.Empty<Link>());
        }

        [HttpGet]
        [Route("{linkId}")]
        public ActionResult<Link> Get(int userId, int linkId)
        {
            var link = _repository.GetLink<ClassicLink>(userId, linkId);

            if (link != null)
            {
                return Ok(link);
            }

            return NotFound(null);
        }
    }
}