using System.Text.Json;

namespace IntegrationTests
{
    public static class Helpers
    {
        public static T Deserialize<T>(string str)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<T>(str, options);
        }
    }
}
