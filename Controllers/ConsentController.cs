using BurnoutSurveyMVC.Data;
using BurnoutSurveyMVC.Models;
using BurnoutSurveyMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BurnoutSurveyMVC.Controllers
{
    public class ConsentController : Controller
    {
        private readonly PesquisaContext _db;
        public ConsentController(PesquisaContext db) { _db = db; }

        [HttpGet]
        public IActionResult Index() => View(new ConsentViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ConsentViewModel model)
        {
            if (!model.AceitouTCLE)
                ModelState.AddModelError(nameof(model.AceitouTCLE), "Você deve aceitar o TCLE para prosseguir.");

            if (!ModelState.IsValid) return View(model);

            var participante = new Participante
            {
                AceitouTCLE = true,
                Ip = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Idade = model.Idade,
                Genero = model.Genero,
                TempoAtuacao = model.TempoAtuacao,
                RegimeTrabalho = model.RegimeTrabalho,
                CargaHoraria = model.CargaHoraria
            };
            _db.Participantes.Add(participante);
            _db.SaveChanges();

            return RedirectToAction("Index", "MBI", new { id = participante.Id });
        }
    }
}
