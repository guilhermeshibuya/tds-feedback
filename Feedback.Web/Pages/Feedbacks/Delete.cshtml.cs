using Feedback.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Feedback.Web.Pages.Feedbacks
{
    public class DeleteModel : PageModel
    {
        public FeedbackModel Feedback { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Uri url = new Uri($"https://localhost:7185/Feedbacks/{id}");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url.ToString());

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                Feedback = JsonConvert.DeserializeObject<FeedbackModel>(responseContent);
            }      

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            Uri url = new Uri($"https://localhost:7185/Feedbacks/{id}");

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.DeleteAsync(url.ToString());

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return RedirectToPage("/Feedbacks/Index");
            }
            return Page();
        }
    }
}
