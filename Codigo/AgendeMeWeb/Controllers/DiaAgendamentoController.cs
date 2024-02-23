using AgendeMeWeb.Helpers;
using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers
{
    [Authorize(Roles = $"{Papeis.GestorOrgao}, {Papeis.GestorPrefeitura}")]
    public class DiaAgendamentoController : BaseController
    {
        private readonly IDiaAgendamentoService _diaAgendamentoService;
        private readonly IMapper _mapper;

        public DiaAgendamentoController(IDiaAgendamentoService diaAgendamentoService, IMapper mapper)
        {
            _diaAgendamentoService = diaAgendamentoService;
            _mapper = mapper;
        }

        // GET: DiaAgendamentoController
        public ActionResult Index()
        {
            SetLayout();
            var listaDiaAgendamento = _diaAgendamentoService.GetAll();
            var listaDiaAgendamentoModel = _mapper.Map<List<DiaAgendamentoViewModel>>(listaDiaAgendamento);
            return View(listaDiaAgendamentoModel);
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
            SetLayout();
            return View();
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
                return View();
            }
        }

        // GET: DiaAgendamentoController/Edit/5
        public ActionResult Edit(int id)
        {
            SetLayout();
            Diaagendamento diaAgendamento = _diaAgendamentoService.Get(id);
            DiaAgendamentoViewModel diaAgendamentoModel = _mapper.Map<DiaAgendamentoViewModel>(diaAgendamento);
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
                return View();
            }
        }

        // GET: DiaAgendamentoController/Delete/5
        public ActionResult Delete(int id)
        {
            SetLayout();
            Diaagendamento diaAgendamento = _diaAgendamentoService.Get(id);
            DiaAgendamentoViewModel diaAgendamentoModel = _mapper.Map<DiaAgendamentoViewModel>(diaAgendamento);
            return View(diaAgendamentoModel);
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
