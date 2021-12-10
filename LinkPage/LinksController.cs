using LinkPage.Links;
using Microsoft.AspNetCore.Mvc;

namespace LinkPage
{
    [ApiController]
    [Route("v1/users/{userId}/links")]
    public class LinksController : ControllerBase
    {
        private readonly LinksRepository _repository;

        public LinksController(LinksRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Link>> Get(int userId)
        {
            var links = _repository.GetLinks(userId);

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
            var link = _repository.GetLink(userId, linkId);

            if (link != null)
            {
                return Ok((object)link);
            }

            return NotFound(null);
        }
    }
}