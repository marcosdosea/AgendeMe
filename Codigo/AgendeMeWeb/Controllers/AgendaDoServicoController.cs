using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers
{
    public class AgendaDoServicoController : Controller
    {
        private readonly IAgendaDoServicoService _agendaDoServicoService;
        private readonly IMapper _mapper;

        public AgendaDoServicoController(IAgendaDoServicoService agendaDoServicoService, IMapper mapper)
        {
            _agendaDoServicoService = agendaDoServicoService;
            _mapper = mapper;
        }

        // GET: AgendaDoServicoController
        public ActionResult Index()
        {
            var listaAgendasDosServicos = _agendaDoServicoService.GetAll();
            var listaAgendasDosServicosModel = _mapper.Map<List<AgendaDoServicoViewModel>>(listaAgendasDosServicos);
            return View(listaAgendasDosServicosModel);
        }

        // GET: AgendaDoServicoController/Details/5
        public ActionResult Details(int id)
        {
            Agendadoservico cargo = _agendaDoServicoService.Get(id);
            AgendaDoServicoViewModel AgendaDoServicoModel = _mapper.Map<AgendaDoServicoViewModel>(cargo);
            return View(AgendaDoServicoModel);
        }

        // GET: AgendaDoServicoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgendaDoServicoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AgendaDoServicoViewModel agendaDoServicoModel)
        {
            if (ModelState.IsValid)
            {
                var agendaDoServico = _mapper.Map<Agendadoservico>(agendaDoServicoModel);
                _agendaDoServicoService.Create(agendaDoServico);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: AgendaDoServicoController/Edit/5
        public ActionResult Edit(int id)
        {
            Agendadoservico cargo = _agendaDoServicoService.Get(id);
            AgendaDoServicoViewModel AgendaDoServicoModel = _mapper.Map<AgendaDoServicoViewModel>(cargo);
            return View(AgendaDoServicoModel);
        }

        // POST: AgendaDoServicoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AgendaDoServicoViewModel agendaDoServicoModel)
        {
            if (ModelState.IsValid)
            {
                var agendaDoServico = _mapper.Map<Agendadoservico>(agendaDoServicoModel);
                _agendaDoServicoService.Edit(agendaDoServico);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: AgendaDoServicoController/Delete/5
        public ActionResult Delete(int id)
        {
            Agendadoservico cargo = _agendaDoServicoService.Get(id);
            AgendaDoServicoViewModel AgendaDoServicoModel = _mapper.Map<AgendaDoServicoViewModel>(cargo);
            return View(AgendaDoServicoModel);
        }

        // POST: AgendaDoServicoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AgendaDoServicoViewModel agendaDoServicoModel)
        {
            try
            {
                _agendaDoServicoService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
