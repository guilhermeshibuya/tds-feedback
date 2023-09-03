using Feedback.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Feedback.Web.Pages.Feedbacks
{
    public class CreateModel : PageModel
    {
        public FeedbackModel Feedback { get; set; } = new();

        public async Task<IActionResult> OnPostAsync(FeedbackModel feedback)
        {
            if (!ModelState.IsValid) return Page();
            
            Uri url = new Uri("https://localhost:7185/Feedbacks");
            HttpContent content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.PostAsync(url.ToString(), content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return RedirectToPage("/Feedbacks/Index");
            }
            return Page();
        }
    }
}
