using System.ComponentModel.DataAnnotations;

namespace BurnoutSurveyMVC.ViewModels
{
    public class ConsentViewModel
    {
        [Required(ErrorMessage = "Você deve aceitar o TCLE.")]
        public bool AceitouTCLE { get; set; }
        public string? Ip { get; set; }

        [Required(ErrorMessage = "Informe sua idade.")]
        [Range(16, 100, ErrorMessage = "Idade deve estar entre 16 e 100.")]
        public int? Idade { get; set; }

        [Required(ErrorMessage = "Informe seu gênero.")]
        public string? Genero { get; set; }

        [Required(ErrorMessage = "Informe o tempo de atuação.")]
        public string? TempoAtuacao { get; set; }

        [Required(ErrorMessage = "Informe o regime de trabalho.")]
        public string? RegimeTrabalho { get; set; }

        [Required(ErrorMessage = "Informe a carga horária semanal.")]
        public string? CargaHoraria { get; set; }
    }
}
