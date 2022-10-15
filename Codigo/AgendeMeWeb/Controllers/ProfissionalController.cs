using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgendeMeWeb.Controllers
{
    public class ProfissionalController : Controller
    {
        private readonly ICidadaoService _cidadaoService;
        private readonly ICargoService _cargoService;
        private readonly IPrefeituraService _prefeituraService;
        private readonly IMapper _mapper;

        public ProfissionalController(ICidadaoService cidadaoService,
            ICargoService cargoService, IPrefeituraService prefeituraService,
            IMapper mapper)
        {
            _cidadaoService = cidadaoService;
            _cargoService = cargoService;
            _prefeituraService = prefeituraService;
            _mapper = mapper;
        }
        // GET: ProfissionalController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProfissionalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProfissionalController/Create
        public ActionResult AddProfissional()
        {
            ProfissionalViewModel profissionalViewModel = new ProfissionalViewModel();

            IEnumerable<Cargo> listaCargos = _cargoService.GetAll();
            IEnumerable<Prefeitura> listaPrefeituras = _prefeituraService.GetAll();

            profissionalViewModel.ListaCargos = new SelectList(listaCargos, "Id", "Nome", null);
            profissionalViewModel.ListaPrefeituras = new SelectList(listaPrefeituras, "Id", "Nome");

            return View(profissionalViewModel);
        }

        // POST: ProfissionalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProfissional(ProfissionalViewModel profissionalModel)
        {

            _cidadaoService.AddProfissional(profissionalModel.IdCidadao, profissionalModel.IdPrefeitura, profissionalModel.IdCargo);

            return RedirectToAction(nameof(Index));

        }

        // GET: ProfissionalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProfissionalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfissionalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfissionalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
