using LinkPage.Links;
using System.Collections.Generic;

namespace IntegrationTests
{
    public class TestData
    {
        public TestData()
        {
            Links = new List<Link>();
        }

        public IEnumerable<Link> Links { get; set; }
    }
}
