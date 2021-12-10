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
        public IEnumerable<ClassicLinkModel> Get() => _repository.Get();

        [HttpGet]
        [Route("{id}")]
        public ClassicLinkModel Get(int id) => _repository.Get(id);
    }
}