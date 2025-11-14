using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAL
{
    public class DAL_Gemini
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly string _model = "gemini-2.5-flash";

        public DAL_Gemini(string apiKey)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _httpClient = new HttpClient();
        }

        // Gửi prompt tới Gemini API và nhận phản hồi
        public async Task<string> GenerateContentAsync(string prompt)
        {
            var url = $"https://generativelanguage.googleapis.com/v1/models/{_model}:generateContent?key={_apiKey}";


            var body = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error ({response.StatusCode}): {err}");
            }

            var result = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(result);

            try
            {
                string text = data.candidates[0].content.parts[0].text.ToString();
                return text;
            }
            catch
            {
                return "Không nhận được phản hồi hợp lệ từ API.";
            }
        }
    }
}
