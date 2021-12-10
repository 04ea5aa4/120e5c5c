﻿using System.Text.Json;

namespace IntegrationTests
{
    public static class Helpers
    {
        public static string Serialize<T>(T obj)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Serialize<T>(obj, options);
        }

        public static T? Deserialize<T>(string str)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<T>(str, options);
        }
    }
}
