using LinkPage.Links.Classic;
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
        public ActionResult<IEnumerable<ClassicLink>> Get(int userId)
        {
            var links = _repository
                .GetLinks(userId)
                .Select(link => (object)link);

            if (links.Any())
            {
                return Ok(links);
            }

            return NotFound(Array.Empty<ClassicLink>());
        }
    }
}