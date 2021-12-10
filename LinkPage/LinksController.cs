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
        public ActionResult<IEnumerable<Link>> Get()
        {
            var links = _repository.Get();

            if (links.Any())
            {
                return Ok(links.Select(link => (object)link));
            }

            return NotFound(Array.Empty<Link>());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Link> Get(int id)
        {
            var link = _repository.Get(id);

            if (link != null)
            {
                return Ok((object)link);
            }

            return NotFound(null);
        }
    }
}