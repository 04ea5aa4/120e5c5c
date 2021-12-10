using LinkPage.Links;
using System.Collections.Generic;

namespace IntegrationTests
{
    public class TestData
    {
        public TestData()
        {
            ClassicLinks = new List<ClassicLinkModel>();
        }

        public IEnumerable<ClassicLinkModel> ClassicLinks { get; set; }
    }
}
