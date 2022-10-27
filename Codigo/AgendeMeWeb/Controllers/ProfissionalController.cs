using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.DTO;
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
            var listaProfissionais = _cidadaoService.GetAllProfissional();

            return View(listaProfissionais);
        }

        // GET: ProfissionalController/Details/5
        public ActionResult Details(int IdProfissional, int IdCargo, int IdPrefeitura)
        {
            Cargoprofissionalprefeitura profissional = _cidadaoService.GetProfissional(IdProfissional, IdCargo, IdPrefeitura);
            ProfissionalViewModel profissionalViewModel = _mapper.Map<ProfissionalViewModel>(profissional);

            Cidadao cidadao = _cidadaoService.Get(IdProfissional);
            profissionalViewModel.NomeProfissional = cidadao.Nome;

            Prefeitura prefeitura = _prefeituraService.Get(IdPrefeitura);
            profissionalViewModel.NomePrefeitura = prefeitura.Nome;

            Cargo cargo = _cargoService.Get(IdCargo);
            profissionalViewModel.NomeCargo = cargo.Nome;

            return View(profissionalViewModel);
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
            _cidadaoService.AddProfissional(profissionalModel.IdProfissional,
                                            profissionalModel.IdPrefeitura,
                                            profissionalModel.IdCargo);
            return RedirectToAction(nameof(Index));
        }

        // GET: ProfissionalController/Edit/5
        public ActionResult Edit(int IdProfissional, int IdCargo, int IdPrefeitura)
        {
            Cargoprofissionalprefeitura profissional = _cidadaoService.GetProfissional(IdProfissional, IdCargo, IdPrefeitura);
            ProfissionalViewModel profissionalViewModel = _mapper.Map<ProfissionalViewModel>(profissional);

            Cidadao cidadao = _cidadaoService.Get(IdProfissional);
            profissionalViewModel.NomeProfissional = cidadao.Nome;

            Prefeitura prefeitura = _prefeituraService.Get(IdPrefeitura);
            profissionalViewModel.NomePrefeitura = prefeitura.Nome;

            Cargo cargo = _cargoService.Get(IdCargo);
            profissionalViewModel.NomeCargo = cargo.Nome;

            IEnumerable<Cargo> listaCargos = _cargoService.GetAll();
            
            profissionalViewModel.ListaCargos = new SelectList(listaCargos, "Id", "Nome", null);

            return View(profissionalViewModel);
        }

        // POST: ProfissionalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int IdCargo, ProfissionalViewModel profissionalModel)
        {   //TODO
            try
            {
                if (ModelState.IsValid)
                {
                    Cargoprofissionalprefeitura profissional = _mapper.Map<Cargoprofissionalprefeitura>(profissionalModel);
                    _cidadaoService.EditProfissional(profissional);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfissionalController/Delete/5
        public ActionResult Delete(int IdProfissional, int IdCargo, int IdPrefeitura)
        {
            Cargoprofissionalprefeitura profissional = _cidadaoService.GetProfissional(IdProfissional, IdCargo, IdPrefeitura);
            ProfissionalViewModel profissionalViewModel = _mapper.Map<ProfissionalViewModel>(profissional);

            Cidadao cidadao = _cidadaoService.Get(IdProfissional);
            profissionalViewModel.NomeProfissional = cidadao.Nome;

            Prefeitura prefeitura = _prefeituraService.Get(IdPrefeitura);
            profissionalViewModel.NomePrefeitura = prefeitura.Nome;

            Cargo cargo = _cargoService.Get(IdCargo);
            profissionalViewModel.NomeCargo = cargo.Nome;

            return View(profissionalViewModel);
        }

        // POST: ProfissionalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int IdProfissional, ProfissionalViewModel profissional)
        { //ta chegnado vazio
            _cidadaoService.DeleteProfissional(IdProfissional, profissional.IdCargo, profissional.IdPrefeitura);
            return RedirectToAction(nameof(Index));

        }
    }
}
