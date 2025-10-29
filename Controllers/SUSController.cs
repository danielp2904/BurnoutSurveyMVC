using BurnoutSurveyMVC.Data;
using BurnoutSurveyMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BurnoutSurveyMVC.Controllers
{
    public class SUSController : Controller
    {
        private readonly PesquisaContext _db;
        public SUSController(PesquisaContext db) { _db = db; }

        private static readonly string[] Textos = new[]
        {
            "Eu usaria este site com frequência.",
            "O site é desnecessariamente complexo.",
            "O site me pareceu fácil de usar.",
            "Vou precisar de ajuda técnica para usar este site.",
            "As funcionalidades estão bem integradas.",
            "Há inconsistências no site.",
            "A maioria das pessoas aprenderia a usar este site rapidamente.",
            "O site é muito difícil de usar.",
            "Eu me senti confiante usando o site.",
            "Precisei aprender várias coisas antes de conseguir usar."
        };

        [HttpGet]
        public IActionResult Index(Guid id)
        {
            var vm = new SUSViewModel
            {
                ParticipanteId = id,
                Itens = Enumerable.Range(1,10).Select(i => new SUSItem { Numero = i, Texto = Textos[i-1] }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SUSViewModel model)
        {
            if (model.Itens.Any(i => i.Valor is null))
            {
                ModelState.AddModelError("", "Responda todas as questões para prosseguir.");
                return View(model);
            }

            foreach (var i in model.Itens)
                _db.RespostasSUS.Add(new Models.RespostaSUS { ParticipanteId = model.ParticipanteId, NumeroQuestao = i.Numero, Valor = i.Valor!.Value });

            await _db.SaveChangesAsync();
            return RedirectToAction("Resumo", "Resultado", new { id = model.ParticipanteId });
        }
    }
}
