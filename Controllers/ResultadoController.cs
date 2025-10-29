using BurnoutSurveyMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace BurnoutSurveyMVC.Controllers
{
    public class ResultadoController : Controller
    {
        private readonly ScoringService _scoring;
        public ResultadoController(ScoringService scoring) { _scoring = scoring; }

        [HttpGet]
        public async Task<IActionResult> Resumo(Guid id)
        {
            var (ex, dp, rpInv, classe) = await _scoring.CalcularMBIAsync(id);
            var sus = await _scoring.CalcularSUSAsync(id);
            ViewBag.Exaustao = ex;
            ViewBag.Despersonalizacao = dp;
            ViewBag.RealizacaoInv = rpInv;
            ViewBag.Classificacao = classe;
            ViewBag.SUS = sus;
            ViewBag.ParticipanteId = id;
            return View();
        }
    }
}
