using Feedback.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Feedback.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<FeedbackModel> Feedbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("FeedbacksDb");
        }
    }
}
