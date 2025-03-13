using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace StrokeButTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "AIzaSyD6HYrV5S5c5eLClYYJ8ywItf2gqQNuQnM"; // استبدلها بمفتاحك الحقيقي
        private readonly string _endpoint;

        public ChatBotController()
        {
            _httpClient = new HttpClient();
            _endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}";
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskChatbot([FromBody] ChatRequest request)
        {
            var prompt = $"You are a medical assistant specializing in stroke. You must answer every question using reliable medical knowledge and always respond in the same language as the inquiry. Internally analyze each word of the question to determine its relationship to stroke using your reasoning capabilities, but do not reveal any internal chain-of-thought or analysis to the user.\r\n\r\nProvide a clear, concise, and final answer. If the question is ambiguous or unclear, generate a clarifying question by proposing a likely interpretation (for example, 'Do you mean [proposed interpretation]?') along with a suggested answer if a key term appears related to stroke. If the user confirms with 'yes', then provide the detailed answer; if the user replies 'no', ask for further clarification or propose an alternative clarifying question.\r\n\r\nIf the question pertains to a medical emergency, include a brief disclaimer that your response is for informational purposes only and advise the user to seek immediate professional help.\r\n\r\nIf the question is completely unrelated to stroke, simply respond: 'I am a model specialized in stroke.'\r\n\r\nAdditionally, if the question has multiple components, organize your answer using clear sections or bullet points for clarity. Question: \"{request.Question}\"";

            var requestBody = new
            {
                contents = new[]
                {
                new { parts = new[] { new { text = prompt } } }
            }
            };

            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var response = await _httpClient.PostAsync(_endpoint, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();

            return Ok(responseBody);
        }
    }

    public class ChatRequest
    {
        public string Question { get; set; }
    }
}
