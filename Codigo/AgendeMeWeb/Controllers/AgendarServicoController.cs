using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers
{
    public class AgendarServicoController : Controller
    {
        private readonly IAgendamentoService _agendamentoService;
        private readonly IPrefeituraService _prefeituraService;
        private readonly IAreaDeServicoService _areaDeServicoService;
        private readonly IServicoPublicoService _servicoPublicoService;
        private readonly IOrgaoPublicoService _orgaoPublicoService;
        private readonly IAgendaDoServicoService _agendaDoServicoService;
        private readonly IMapper _mapper;

        public AgendarServicoController(IAgendamentoService agendamentoService,
                                        IPrefeituraService prefeituraService,
                                        IAreaDeServicoService areaDeServicoService,
                                        IServicoPublicoService servicoPublicoService,
                                        IOrgaoPublicoService orgaoPublicoService,
                                        IAgendaDoServicoService agendaDoServicoService,
                                        IMapper mapper)
        {
            _agendamentoService = agendamentoService;
            _prefeituraService = prefeituraService;
            _areaDeServicoService = areaDeServicoService;
            _servicoPublicoService = servicoPublicoService;
            _orgaoPublicoService = orgaoPublicoService;
            _agendaDoServicoService = agendaDoServicoService;
            _mapper = mapper;
        }

        // GET: AgendarServicoController
        public ActionResult Index()
        {
            var listaPrefeitura = _prefeituraService.GetAll();
            var listaPrefeituraModel = _mapper.Map<List<PrefeituraViewModel>>(listaPrefeitura);
            return View(listaPrefeituraModel);
        }

        public ActionResult List()
        {
            var listaAgendamentos = _agendamentoService.GetAll();
            var listaAgendamentosModel = _mapper.Map<List<AgendarServicoViewModel>>(listaAgendamentos);
            return View(listaAgendamentosModel);
        }

        // GET: AgendarServicoController/Details/5
        public ActionResult Details(int id)
        {
            Agendamento agendamento = _agendamentoService.Get(id);
            AgendarServicoViewModel agendamentoModel = _mapper.Map<AgendarServicoViewModel>(agendamento);
            return View(agendamentoModel);
        }

        // GET: AgendarServicoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgendarServicoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AgendarServicoViewModel agendamentoModel)
        {
            try
            {
                var agendamento = _mapper.Map<Agendamento>(agendamentoModel);
                _agendamentoService.Create(agendamento);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AgendarServicoController/Edit/5
        public ActionResult Edit(int id)
        {
            Agendamento agendamento = _agendamentoService.Get(id);
            AgendarServicoViewModel agendamentoModel = _mapper.Map<AgendarServicoViewModel>(agendamento);
            return View(agendamentoModel);
        }

        public ActionResult AtenderCidadao(int id)
        {
            Agendamento agendamento = _agendamentoService.Get(id);
            AgendarServicoViewModel agendamentoModel = _mapper.Map<AgendarServicoViewModel>(agendamento);
            return View(agendamentoModel);
        }

        // POST: AgendarServicoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AgendarServicoViewModel agendamentoModel)
        {
            try
            {
                var agendamento = _mapper.Map<Agendamento>(agendamentoModel);
                _agendamentoService.Edit(agendamento);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AgendarServicoController/Delete/5
        public ActionResult Delete(int id)
        {
            Agendamento agendamento = _agendamentoService.Get(id);
            AgendarServicoViewModel agendamentoModel = _mapper.Map<AgendarServicoViewModel>(agendamento);
            return View(agendamentoModel);
        }

        // POST: AgendarServicoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AgendarServicoViewModel agendamentoModel)
        {
            try
            {
                _agendamentoService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AgendarRetorno(int id)
        {
            Agendamento agendamento = _agendamentoService.Get(id);
            AgendarServicoViewModel agendamentoModel = _mapper.Map<AgendarServicoViewModel>(agendamento);
            agendamentoModel.IdRetorno = agendamentoModel.Id;
            return View(agendamentoModel);
        }

        [HttpGet]
        public ActionResult AreasDeServico(int id)
        {
            var listaAreasDeServico = _areaDeServicoService.GetAllByIdPrefeitura(id);
            var listaAreasDeServicoModel = _mapper.Map<List<AreaDeServicoViewModel>>(listaAreasDeServico);
            return PartialView(listaAreasDeServicoModel);
        }

        [HttpGet]
        public ActionResult ServicosPublicos(int id, string nomeArea, string iconeArea)
        {
            ViewBag.nomeAreaDeServico = nomeArea;
            ViewBag.iconeAreaDeServico = iconeArea;
            var listaServicoPublico = _servicoPublicoService.GetAllByIdArea(id);
            return PartialView(listaServicoPublico);
        }

        [HttpGet]
        public ActionResult OrgaosPublicos(string nomeServico, string iconeServico)
        {
            ViewBag.iconeServicoPublico = iconeServico;
            ViewBag.nomeServicoPublico = nomeServico;
            var listaOrgaosPublicoDTO = _orgaoPublicoService.GetAllByNomeServicoPublico(nomeServico);
            return PartialView(listaOrgaosPublicoDTO);
        }

        [HttpGet]
        public ActionResult AgendasDoServicoDias(int idServico, string nomeOrgao, string nomeServico)
        {
            ViewBag.nomeOrgaoPublico = nomeOrgao;
            ViewBag.nomeServicoPublico = nomeServico;
            var listaAgendasDoServico = _agendaDoServicoService.GetAllDiasByIdServico(idServico);
            return View(listaAgendasDoServico);
        }

        [HttpGet]
        public ActionResult AgendasDoServicoHoras(int idServico, string nomeServico, string iconeServico)
        {
            ViewBag.iconeServicoPublico = iconeServico;
            ViewBag.nomeServicoPublico = nomeServico;
            var listaOrgaosPublico = _orgaoPublicoService.GetAllByNomeServicoPublico(nomeServico);
            var listaOrgaosPublicoModel = _mapper.Map<List<OrgaoPublicoViewModel>>(listaOrgaosPublico);
            return PartialView(listaOrgaosPublicoModel);
        }
    }
}
