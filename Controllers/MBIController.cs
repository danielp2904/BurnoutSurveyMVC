using BurnoutSurveyMVC.Data;
using BurnoutSurveyMVC.Models;
using BurnoutSurveyMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurnoutSurveyMVC.Controllers
{
    public class MBIController : Controller
    {
        private readonly PesquisaContext _db;
        public MBIController(PesquisaContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var groups = await _db.SurveyQuestions.AsNoTracking()
                .Where(q => q.Grupo == QuestionGroup.ExaustaoEmocional || q.Grupo == QuestionGroup.Despersonalizacao || q.Grupo == QuestionGroup.RealizacaoPessoal)
                .OrderBy(q => q.Grupo).ThenBy(q => q.Ordem).ToListAsync();

            var vm = new MBIViewModel { ParticipanteId = id };

            foreach (var grp in groups.GroupBy(g => g.Grupo))
            {
                var titulo = grp.Key switch
                {
                    QuestionGroup.ExaustaoEmocional => "Exaustão emocional",
                    QuestionGroup.Despersonalizacao => "Despersonalização",
                    QuestionGroup.RealizacaoPessoal => "Realização pessoal (inverso)",
                    _ => grp.Key.ToString()
                };
                vm.Grupos.Add(new MBIGroupVM
                {
                    Titulo = titulo,
                    Explicacao = grp.First().ExplicacaoTopo,
                    Itens = grp.Select(q => new MBIItem { QuestionId = q.Id, Codigo = q.Codigo, Texto = q.Texto }).ToList()
                });
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(MBIViewModel model)
        {
            if (model.Grupos.SelectMany(g => g.Itens).Any(i => i.Valor is null))
            {
                ModelState.AddModelError("", "Responda todas as questões para prosseguir.");
                return View(model);
            }

            foreach (var item in model.Grupos.SelectMany(g => g.Itens))
            {
                _db.RespostasMBI.Add(new RespostaMBI
                {
                    ParticipanteId = model.ParticipanteId,
                    QuestionId = item.QuestionId,
                    Valor = item.Valor!.Value
                });
            }
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "SUS", new { id = model.ParticipanteId });
        }
    }
}
