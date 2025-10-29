using System.ComponentModel.DataAnnotations;

namespace BurnoutSurveyMVC.Models
{
    public class Participante
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
        public string? Ip { get; set; }

        public int? Idade { get; set; }
        public string? Genero { get; set; }
        public string? TempoAtuacao { get; set; }
        public string? RegimeTrabalho { get; set; }
        public string? CargaHoraria { get; set; }

        public bool AceitouTCLE { get; set; }

        public virtual ICollection<RespostaMBI> RespostasMBI { get; set; } = new List<RespostaMBI>();
        public virtual ICollection<RespostaSUS> RespostasSUS { get; set; } = new List<RespostaSUS>();
    }
}
