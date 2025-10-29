namespace BurnoutSurveyMVC.ViewModels
{
    public class MBIItem
    {
        public int QuestionId { get; set; }
        public string Codigo { get; set; } = "";
        public string Texto { get; set; } = "";
        public int? Valor { get; set; }
    }

    public class MBIGroupVM
    {
        public string Titulo { get; set; } = "";
        public string? Explicacao { get; set; }
        public List<MBIItem> Itens { get; set; } = new();
    }

    public class MBIViewModel
    {
        public Guid ParticipanteId { get; set; }
        public List<MBIGroupVM> Grupos { get; set; } = new();
    }
}
