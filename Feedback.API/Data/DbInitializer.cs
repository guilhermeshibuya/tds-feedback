using Feedback.API.Models;

namespace Feedback.API.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context) 
        {
            if (context.Feedbacks!.Any())
            {
                return;
            }

            var feedbacks = new FeedbackModel[]
            {
                new FeedbackModel
                {
                    NomeCliente = "Guilherme Shibuya",
                    EmailCliente = "teste@gmail.com",
                    DataFeedback = DateTime.Parse("2023-08-29"),
                    Comentario = "Gostei muito do produto!",
                    Avaliacao = 5,
                }
            };

            context.AddRange(feedbacks);
            context.SaveChanges();
        }
    }
}
