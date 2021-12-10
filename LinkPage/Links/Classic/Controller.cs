using Microsoft.AspNetCore.Mvc;

namespace LinkPage.Links.Classic
{
    [ApiController]
    [Route("v1/users/{userId}/links/classic")]
    public class Controller : ControllerBase
    {
        private readonly IEnumerable<Model> _links = new List<Model>()
        {
            new Model
            {
                Id = 1,
                Title = "Google",
                Url = "https://google.com",
            },
            new Model
            {
                Id = 2,
                Title = "Facebook",
                Url = "https://facebook.com",
            },
        };

        public Controller() { }

        [HttpGet]
        public IEnumerable<Model> Get() => _links;

        [HttpGet]
        [Route("{id}")]
        public Model Get(int id) =>
            _links
                .Where(link => link.Id == id)
                .FirstOrDefault();
    }
}