namespace BurnoutSurveyMVC.ViewModels
{
    public class SUSItem
    {
        public int Numero { get; set; }
        public int? Valor { get; set; }
        public string Texto { get; set; } = "";
    }

    public class SUSViewModel
    {
        public Guid ParticipanteId { get; set; }
        public List<SUSItem> Itens { get; set; } = new();
    }
}
