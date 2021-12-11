using LinkPage.Links.Classic;
using Microsoft.AspNetCore.Mvc;

namespace LinkPage.Links.Shows
{
    [ApiController]
    [Route("v1/users/{userId}/links/shows")]
    public class ShowsLinkController : ControllerBase
    {
        private readonly LinksRepository _repository;

        public ShowsLinkController(LinksRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult<ShowsLink> Post(int userId, ShowsLink link)
        {
            var validationResult = new ShowsLinkValidator().Validate(link);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }

            var createdLink = _repository.Add(userId, link);
            var newUrl = $"v1/users/{createdLink.UserId}/links/shows/{createdLink.LinkId}";

            return Created(newUrl, createdLink);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClassicLink>> Get(int userId)
        {
            var links = _repository
                .GetLinks(userId)
                .OfType<ShowsLink>();

            if (links.Any())
            {
                return Ok(links.Select(link => (object)link));
            }

            return NotFound(Array.Empty<ClassicLink>());
        }

        [HttpGet]
        [Route("{linkId}")]
        public ActionResult<ClassicLink> Get(int userId, int linkId)
        {
            var link = _repository.GetLink<ShowsLink>(userId, linkId);

            if (link != null)
            {
                return Ok(link);
            }

            return NotFound(null);
        }
    }
}