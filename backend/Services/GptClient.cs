// File: Services/GptClient.cs
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MTSTrueTechHack.Backend.Models.Dtos;

namespace MTSTrueTechHack.Backend.Services
{
    public class GptClient
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOpts = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public GptClient(HttpClient http, IConfiguration config)
        {
            _http = http;
            // BaseUrl in appsettings.json should end with a slash: https://api.openai.com/v1/
            _http.BaseAddress = new Uri(config["Gpt:BaseUrl"]!);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config["Gpt:ApiKey"]!);
        }

        public async Task<string> GenerateSchemaAsync(CreateSchemaDto dto)
        {
            var system = new { role = "system", content = "You are a JSON schema generator." };
            var user   = new { role = "user",   content = $"Create a JSON Schema (draft-07) for an object with name '{dto.Name}' and description '{dto.Description}'." };

            var payload = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] { system, user },
                temperature = 0.0
            };

            var httpContent = new StringContent(
                JsonSerializer.Serialize(payload, _jsonOpts),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _http.PostAsync("chat/completions", httpContent);

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Console.WriteLine("[Warning] Rate limited by OpenAI, returning empty schema {}");
                return "{}";
            }

            response.EnsureSuccessStatusCode();
            var raw = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(raw);
            var content = doc.RootElement
                             .GetProperty("choices")[0]
                             .GetProperty("message")
                             .GetProperty("content")
                             .GetString()!
                             .Trim();
            return content;
        }

        public async Task<string> ContinueChatAsync(ChatDto dto)
        {
            var system = new { role = "system", content = $"You are a JSON schema assistant. Schema: {dto.JsonSchema}" };
            var user   = new { role = "user",   content = dto.Message };

            var messages = dto.History
                .Select(m => new { role = m.IsFromUser ? "user" : "assistant", content = m.Text })
                .Concat(new[] { system, user })
                .ToArray();

            var payload = new
            {
                model = "gpt-3.5-turbo",
                messages,
                temperature = 0.7
            };

            var httpContent = new StringContent(
                JsonSerializer.Serialize(payload, _jsonOpts),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _http.PostAsync("chat/completions", httpContent);

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Console.WriteLine("[Warning] Rate limited by OpenAI, returning empty message");
                return string.Empty;
            }

            response.EnsureSuccessStatusCode();
            var raw = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(raw);
            var content = doc.RootElement
                             .GetProperty("choices")[0]
                             .GetProperty("message")
                             .GetProperty("content")
                             .GetString()!
                             .Trim();
            return content;
        }
    }
}
