using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.DTO;
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
        private readonly IDiaAgendamentoService _diaAgendamentoService;
        private readonly ICidadaoService _cidadaoService;
        private readonly IMapper _mapper;

        public AgendarServicoController(IAgendamentoService agendamentoService,
                                        IPrefeituraService prefeituraService,
                                        IAreaDeServicoService areaDeServicoService,
                                        IServicoPublicoService servicoPublicoService,
                                        IOrgaoPublicoService orgaoPublicoService,
                                        IDiaAgendamentoService diaAgendamentoService,
                                        ICidadaoService cidadaoService,
                                        IMapper mapper)
        {
            _agendamentoService = agendamentoService;
            _prefeituraService = prefeituraService;
            _areaDeServicoService = areaDeServicoService;
            _servicoPublicoService = servicoPublicoService;
            _orgaoPublicoService = orgaoPublicoService;
            _diaAgendamentoService = diaAgendamentoService;
            _cidadaoService = cidadaoService;
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
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View(nameof(List));
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
            ViewBag.idPrefeitura = _areaDeServicoService.Get(id).IdPrefeitura;
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
        public ActionResult AgendarServicoDias(int idServico, string nomeOrgao, string nomeServico, int idOrgao)
        {
            ViewBag.nomeOrgaoPublico = nomeOrgao;
            ViewBag.nomeServicoPublico = nomeServico;
            ViewBag.idOrgao = idOrgao;
            var listaDias = _diaAgendamentoService.GetAllDiasByIdServico(idServico);
            return PartialView(listaDias);
        }

        [HttpGet]
        public ActionResult AgendarServicoHoras(int idServico, DateTime dia,
                                                string nomeDia, string nomeOrgao,
                                                string nomeServico, int idOrgao)
        {
            ViewBag.nomeOrgaoPublico = nomeOrgao;
            ViewBag.nomeServicoPublico = nomeServico;
            ViewBag.nomeDia = nomeDia;
            ViewBag.idOrgao = idOrgao;
            var listaHoras = _diaAgendamentoService.GetAllHorasByIdServicoAndDia(idServico, dia);
            return PartialView(listaHoras);
        }

        [HttpGet]
        public ActionResult ConfirmarAgendamento(int idDiaAgendamento)
        {
            var dadosAgendamento = _diaAgendamentoService.GetDadosAgendamento(idDiaAgendamento);
            return PartialView(dadosAgendamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarAgendamento(AgendarServicoViewModel agendamentoModel)
        {
            try
            {
                var agendamento = _mapper.Map<Agendamento>(agendamentoModel);
                var id = _agendamentoService.Create(agendamento);

                if (id.Result == -1)
                {
                    ViewBag.erro = "Ocorreu um problema, por favor tente novamente!";
                    var dadosAgendamento = _diaAgendamentoService.GetDadosAgendamento(agendamentoModel.IdDiaAgendamento);
                    return View(dadosAgendamento);
                }
                else
                {
                    return RedirectToAction(nameof(AgendamentoConfirmado), new { id = id.Result });
                }

            }
            catch
            {
                ViewBag.erro = "O campo CPF é obrigatório";
                var dadosAgendamento = _diaAgendamentoService.GetDadosAgendamento(agendamentoModel.IdDiaAgendamento);
                return View(dadosAgendamento);
            }

        }

        [HttpGet]
        public ActionResult GetCidadao(string CPF)
        {
            CidadaoDTO cidadaoDTO = _cidadaoService.GetByCPF(CPF);
            return PartialView(cidadaoDTO);
        }

        [HttpGet]
        public ActionResult AgendamentoConfirmado(int id)
        {
            AgendamentoDTO agendamento = _agendamentoService.GetDados(id);
            return View(agendamento);
        }
    }
}
