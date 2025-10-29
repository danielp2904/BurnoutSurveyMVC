using System.ComponentModel.DataAnnotations;

namespace BurnoutSurveyMVC.Models
{
    public class SurveyQuestion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Codigo { get; set; } = "";
        [Required]
        public string Texto { get; set; } = "";
        public QuestionGroup Grupo { get; set; }
        public QuestionScale Escala { get; set; } = QuestionScale.MBI_0_6;
        public int Ordem { get; set; }
        public string? ExplicacaoTopo { get; set; }
    }
}
