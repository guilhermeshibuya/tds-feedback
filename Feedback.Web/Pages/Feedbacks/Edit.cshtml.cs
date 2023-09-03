using Feedback.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Feedback.Web.Pages.Feedbacks
{
    public class EditModel : PageModel
    {
        public FeedbackModel Feedback { get; set; } = new();

        public async Task<IActionResult> OnPostAsync(int? id, FeedbackModel feedback)
        {
            if (id == null || feedback == null) return NotFound();

            Uri url = new Uri($"https://localhost:7185/Feedbacks/{id}");
            HttpContent content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.PutAsync(url.ToString(), content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return RedirectToPage("/Feedbacks/Index");
            }
            return Page();
        }
    }
}
