using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurnoutSurveyMVC.Models
{
    public class RespostaMBI
    {
        [Key]
        public int Id { get; set; }
        public Guid ParticipanteId { get; set; }
        [ForeignKey(nameof(ParticipanteId))]
        public Participante? Participante { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public SurveyQuestion? Question { get; set; }

        [Range(0, 6)]
        public int Valor { get; set; }
    }
}
