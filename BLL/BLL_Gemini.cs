using DAL;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Gemini
    {
        private readonly DAL_Gemini _dal;

        public BLL_Gemini(string apiKey)
        {
            _dal = new DAL_Gemini(apiKey);
        }

        // Hàm gọi API và có thể thêm logic xử lý trước/sau
        public async Task<string> TaoNoiDungAsync(string prompt)
        {
            // Có thể xử lý prompt, ví dụ thêm tiền tố, kiểm tra độ dài...
            return await _dal.GenerateContentAsync(prompt);
        }
    }
}
