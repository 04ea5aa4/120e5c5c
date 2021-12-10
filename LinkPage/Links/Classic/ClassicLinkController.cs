using Microsoft.AspNetCore.Mvc;

namespace LinkPage.Links
{
    [ApiController]
    [Route("v1/users/{userId}/links/classic")]
    public class ClassicLinkController : ControllerBase
    {
        private readonly ClassicLinkRepository _repository;

        public ClassicLinkController(ClassicLinkRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClassicLinkModel>> Get()
        {
            var links = _repository.Get();

            if (links.Any())
            {
                return Ok(links);
            }

            return NotFound(links);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ClassicLinkModel> Get(int id)
        {
            var link = _repository.Get(id);

            if (link != null)
            {
                return Ok(link);
            }

            return NotFound(null);
        }
    }
}