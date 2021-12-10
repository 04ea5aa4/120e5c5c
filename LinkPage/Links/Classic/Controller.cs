using Microsoft.AspNetCore.Mvc;

namespace LinkPage.Links.Classic
{
    [ApiController]
    [Route("v1/users/{userId}/links/classic")]
    public class Controller : ControllerBase
    {
        public Controller() { }

        [HttpGet]
        public IEnumerable<Model> Get() => new List<Model>()
        {
            new Model
            {
                Title = "Google",
                Url = "https://google.com",
            },
            new Model
            {
                Title = "Facebook",
                Url = "https://facebook.com",
            },
        };
    }
}