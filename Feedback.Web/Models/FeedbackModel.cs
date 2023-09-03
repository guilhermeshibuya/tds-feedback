using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Feedback.Web.Models
{
    public class FeedbackModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? IdFeedback { get; set; }

        [Required(AllowEmptyStrings = false,
                    ErrorMessage = "Nome é obrigatório")]
        public string? NomeCliente { get; set; }

        public string? EmailCliente { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? DataFeedback { get; set; }

        [Required(ErrorMessage = "Comentário é obrigatório")]
        public string? Comentario { get; set; }

        [Required(ErrorMessage = "Avaliação é obrigatória")]
        [Range(1, 5)]
        public int? Avaliacao { get; set; }
    }
}
