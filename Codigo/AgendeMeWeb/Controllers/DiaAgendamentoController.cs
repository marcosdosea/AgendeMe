using AgendeMeWeb.Helpers;
using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgendeMeWeb.Controllers
{
    [Authorize(Roles = $"{Papeis.GestorOrgao}, {Papeis.GestorPrefeitura}")]
    public class DiaAgendamentoController : BaseController
    {
        private readonly IDiaAgendamentoService _diaAgendamentoService;
        private readonly IServicoPublicoService _servico;
        private readonly IMapper _mapper;

        public DiaAgendamentoController(IDiaAgendamentoService diaAgendamentoService, IServicoPublicoService servico, IMapper mapper)
        {
            _diaAgendamentoService = diaAgendamentoService;
            _mapper = mapper;
            _servico = servico;
        }

        // GET: DiaAgendamentoController
        public ActionResult Index()
        {
            SetLayout();
            var listaDiaAgendamento = _diaAgendamentoService.GetAllByOrgao(Convert.ToInt32(User.FindFirst("IdOrgao")?.Value));
            return View(listaDiaAgendamento);
        }

        // GET: DiaAgendamentoController/Details/5
        public ActionResult Details(int id)
        {
            SetLayout();
            Diaagendamento diaAgendamento = _diaAgendamentoService.Get(id);
            DiaAgendamentoViewModel diaAgendamentoModel = _mapper.Map<DiaAgendamentoViewModel>(diaAgendamento);
            return View(diaAgendamentoModel);
        }

        // GET: DiaAgendamentoController/Create
        public ActionResult Create()
        {
            DiaAgendamentoViewModel agenda = new();
            var servicos = _servico.GetAllByIdOrgao(Convert.ToInt32(User.FindFirst("IdOrgao")?.Value));
            agenda.ListaServicos = new SelectList(servicos, "Id", "Nome", null);
            SetLayout();
            return View(agenda);
        }

        // POST: DiaAgendamentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DiaAgendamentoViewModel diaAgendamentoModel)
        {
            try
            {
                var diaAgendamento = _mapper.Map<Diaagendamento>(diaAgendamentoModel);
                _diaAgendamentoService.Create(diaAgendamento);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var servicos = _servico.GetAllByIdOrgao(Convert.ToInt32(User.FindFirst("IdOrgao")?.Value));
                diaAgendamentoModel.ListaServicos = new SelectList(servicos, "Id", "Nome", null);
                SetLayout();
                return View(diaAgendamentoModel);
            }
        }

        // GET: DiaAgendamentoController/Edit/5
        public ActionResult Edit(int id)
        {
            SetLayout();
            Diaagendamento diaAgendamento = _diaAgendamentoService.Get(id);
            DiaAgendamentoViewModel diaAgendamentoModel = _mapper.Map<DiaAgendamentoViewModel>(diaAgendamento);
            var servicos = _servico.GetAllByIdOrgao(Convert.ToInt32(User.FindFirst("IdOrgao")?.Value));
            diaAgendamentoModel.ListaServicos = new SelectList(servicos, "Id", "Nome", diaAgendamentoModel.IdServicoPublico);
            return View(diaAgendamentoModel);
        }

        // POST: DiaAgendamentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DiaAgendamentoViewModel diaAgendamentoViewModel)
        {
            try
            {
                var diaAgendamento = _mapper.Map<Diaagendamento>(diaAgendamentoViewModel);
                _diaAgendamentoService.Edit(diaAgendamento);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //var servicos = _servico.GetAllByIdOrgao(Convert.ToInt32(User.FindFirst("IdOrgao")?.Value));
                //diaAgendamentoViewModel.ListaServicos = new SelectList(servicos, "Id", "Nome", null);
                SetLayout();
                return View(diaAgendamentoViewModel);
            }
        }

        // GET: DiaAgendamentoController/Delete/5
        public ActionResult Delete(int id)
        {
            SetLayout();
            var agenda = _diaAgendamentoService.GetByOrgao(Convert.ToInt32(User.FindFirst("IdOrgao")?.Value),id);
            return View(agenda);
        }

        // POST: DiaAgendamentoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DiaAgendamentoViewModel diaAgendamentoViewModel)
        {
            try
            {
                _diaAgendamentoService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
