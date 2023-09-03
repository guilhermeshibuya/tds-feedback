using Feedback.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Feedback.Web.Pages.Feedbacks
{
    public class IndexModel : PageModel
    {
        public List<FeedbackModel> FeedbackList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Uri url = new Uri("https://localhost:7185/Feedbacks");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url.ToString());

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                FeedbackList = JsonConvert.DeserializeObject<List<FeedbackModel>>(responseContent);
            }

            return Page();
        }
    }
}
