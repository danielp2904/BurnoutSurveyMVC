using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurnoutSurveyMVC.Models
{
    public class RespostaSUS
    {
        [Key]
        public int Id { get; set; }
        public Guid ParticipanteId { get; set; }
        [ForeignKey(nameof(ParticipanteId))]
        public Participante? Participante { get; set; }

        public int NumeroQuestao { get; set; }
        [Range(1,5)]
        public int Valor { get; set; }
    }
}
