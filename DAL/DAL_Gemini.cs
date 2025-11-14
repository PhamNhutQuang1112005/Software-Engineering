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
        private static readonly HttpClient _httpClient = new HttpClient();

        // Chọn model FLASH
        private readonly string _model = "gemini-2.0-flash";

        public DAL_Gemini(string apiKey)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        public async Task<string> GenerateContentAsync(string prompt)
        {
            var url = $"https://generativelanguage.googleapis.com/v1/models/{_model}:generateContent?key={_apiKey}";

            int retry = 0;

            while (retry < 3)
            {
                // Tạo content mới mỗi lần retry
                var body = new
                {
                    contents = new[]
                    {
                        new {
                            role = "user",
                            parts = new[] { new { text = prompt } }
                        }
                    }
                };

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var raw = await response.Content.ReadAsStringAsync();

                    dynamic data = JsonConvert.DeserializeObject(raw);

                    try
                    {
                        // Format Flash / Gemini 2.x
                        string text = data.candidates[0]
                                           .content
                                           .parts[0]
                                           .text;

                        return text;
                    }
                    catch (Exception)
                    {
                        return "Không parse được dữ liệu trả về từ Gemini Flash.";
                    }
                }
                else
                {
                    string err = await response.Content.ReadAsStringAsync();

                    if ((int)response.StatusCode == 503)
                    {
                        await Task.Delay(1500);
                        retry++;
                        continue;
                    }

                    throw new Exception($"API Error ({response.StatusCode}): {err}");
                }
            }

            return "Gemini Flash đang quá tải, vui lòng thử lại.";
        }
    }
}
