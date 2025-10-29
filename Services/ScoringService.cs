using BurnoutSurveyMVC.Data;
using BurnoutSurveyMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BurnoutSurveyMVC.Services
{
    public class ScoringService
    {
        private readonly PesquisaContext _db;
        public ScoringService(PesquisaContext db) { _db = db; }

        public async Task<(double exaustao, double despersonalizacao, double realizacaoInversa, string classificacao)> CalcularMBIAsync(Guid participanteId)
        {
            var respostas = await _db.RespostasMBI.Include(r => r.Question)
                .Where(r => r.ParticipanteId == participanteId).ToListAsync();

            double MediaGrupo(QuestionGroup g, bool inverso = false)
            {
                var vals = respostas.Where(r => r.Question!.Grupo == g).Select(r => inverso ? (6 - r.Valor) : r.Valor);
                return vals.Any() ? vals.Average() : 0.0;
            }

            var ex = MediaGrupo(QuestionGroup.ExaustaoEmocional);
            var dp = MediaGrupo(QuestionGroup.Despersonalizacao);
            var rpInv = MediaGrupo(QuestionGroup.RealizacaoPessoal, inverso: true);

            string classificacao = ex >= 5 ? "Alto risco" : (ex >= 3 ? "Moderado" : "Baixo risco");
            return (Math.Round(ex,2), Math.Round(dp,2), Math.Round(rpInv,2), classificacao);
        }

        public async Task<double> CalcularSUSAsync(Guid participanteId)
        {
            var respostas = await _db.RespostasSUS.Where(r => r.ParticipanteId == participanteId)
                .OrderBy(r => r.NumeroQuestao).ToListAsync();
            double soma = 0;
            foreach (var r in respostas)
                soma += (r.NumeroQuestao % 2 == 1) ? (r.Valor - 1) : (5 - r.Valor);
            return Math.Round(soma * 2.5, 1);
        }
    }
}
