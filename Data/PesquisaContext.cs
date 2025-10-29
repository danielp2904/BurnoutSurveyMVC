using BurnoutSurveyMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BurnoutSurveyMVC.Data
{
    public class PesquisaContext : DbContext
    {
        public PesquisaContext(DbContextOptions<PesquisaContext> options) : base(options) { }

        public DbSet<Participante> Participantes => Set<Participante>();
        public DbSet<RespostaMBI> RespostasMBI => Set<RespostaMBI>();
        public DbSet<RespostaSUS> RespostasSUS => Set<RespostaSUS>();
        public DbSet<SurveyQuestion> SurveyQuestions => Set<SurveyQuestion>();
    }
}
