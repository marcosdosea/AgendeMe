using System.Security.Claims;
using AgendeMeWeb.Helpers;
using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.DTO;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;

namespace AgendeMeWeb.Controllers
{
    public class AgendarServicoController : BaseController
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
            var cookie = Request.Cookies.FirstOrDefault(c => c.Key == "AgendeMeSession");
            if (cookie.Value == null) 
            {
                ViewData["Layout"] = "_Layout";
                return View(_prefeituraService.GetAllCidade());
            }
            var idPrefeitura = User.FindFirst("Prefeitura")?.Value;
            SetLayout();
            if (string.IsNullOrEmpty(idPrefeitura)) 
            {
                return View();
            }
            var id = Convert.ToInt32(idPrefeitura);
            return RedirectToAction(nameof(AreasDeServico), new { id });
        }

        [HttpGet]
        [Authorize]
        public ActionResult List(int id)
        {
            if (id == 0) {
                return RedirectToAction(nameof(Index));
            }
            int idUser = Convert.ToInt32(User.FindFirstValue("Id"));
            var listaAgendamentos = _agendamentoService.GetAllByUser(idUser, id);
            SetLayout();
            ViewBag.Page = id;
            return View(listaAgendamentos);
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

        [HttpGet]
        [Authorize(Roles = $"{Papeis.Atendente}, {Papeis.GestorOrgao}, {Papeis.GestorPrefeitura}")]
        public ActionResult AtenderCidadao(int? id, string? cpf)
        {
            SetLayout();
            if (cpf != null) 
            {
                ViewData["cpf"] = cpf;
                ViewData["page"] = id ?? 1;
                
                var agendamentos = _agendamentoService.GetAllByCpf(cpf, id ?? 1, Convert.ToInt32(User.FindFirstValue("IdOrgao")));
                ViewData["orgao"] = agendamentos.Agendamentos?.Select(a => a.OrgaoPublico).FirstOrDefault() 
                ?? _orgaoPublicoService.Get(Convert.ToInt32(User.FindFirstValue("IdOrgao"))).Nome;
                return View(agendamentos);
            }
            return View(new AgendamentoPage(){});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Papeis.Atendente}, {Papeis.GestorOrgao}, {Papeis.GestorPrefeitura}")]
        public ActionResult ConfirmarPresenca(int id, string cpf, string page)
        {
            _agendamentoService.AtualizarStatus(id, cpf, "Aguardando Atendimento");
            return RedirectToAction(nameof(AtenderCidadao), new {id = page, cpf = cpf});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Papeis.Atendente}, {Papeis.GestorOrgao}, {Papeis.GestorPrefeitura}")]
        public ActionResult ConfirmarAtendimento(int id, string cpf, string page)
        {
            _agendamentoService.AtualizarStatus(id, cpf, "Atendido");
            return RedirectToAction(nameof(AtenderCidadao), new {id = page, cpf = cpf});
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
            var pref = _prefeituraService.Get(id);
            ViewBag.IconeCidade = pref.Cidade.Substring(0, 2).ToUpper();
            ViewBag.Cidade = $"{pref.Cidade}-{pref.Estado}";
            
            if (string.IsNullOrEmpty(User.FindFirst("Prefeitura")?.Value)) 
            {
                return PartialView(listaAreasDeServicoModel);
            }
            ViewData["View"] = true;

            SetLayout();
            return View(listaAreasDeServicoModel);
        }

        [HttpGet]
        public ActionResult ServicosPublicos(int idArea)
        {
            var area = _areaDeServicoService.Get(idArea);
            ViewBag.nomeAreaDeServico = area.Nome;
            ViewBag.iconeAreaDeServico = area.Icone;
            ViewBag.idPrefeitura = area.IdPrefeitura;
            ViewBag.idArea = area.Id;
            var listaServicoPublico = _servicoPublicoService.GetAllByIdArea(idArea);
            if (!string.IsNullOrEmpty(User.FindFirst("Prefeitura")?.Value)) 
            {
                ViewData["View"] = true;
            }
            return PartialView(listaServicoPublico);
        }

        [HttpGet]
        public ActionResult OrgaosPublicos(int idArea, string nomeServico, string iconeServico)
        {
            ViewBag.iconeServicoPublico = iconeServico;
            ViewBag.nomeServicoPublico = nomeServico;
            ViewBag.idArea = idArea;
            var listaOrgaosPublicoDTO = _orgaoPublicoService.GetAllByNomeServicoPublico(nomeServico);
            return PartialView(listaOrgaosPublicoDTO);
        }

        [HttpGet]
        public ActionResult AgendarServicoDias(int idServico, string nomeOrgao, 
                                               string nomeServico, int idOrgao, 
                                               int idArea, string iconeServico)
        {
            ViewBag.nomeOrgaoPublico = nomeOrgao;
            ViewBag.nomeServicoPublico = nomeServico;
            ViewBag.iconeServico = iconeServico;
            ViewBag.idOrgao = idOrgao;
            ViewBag.idArea = idArea;
            var listaDias = _diaAgendamentoService.GetAllDiasByIdServico(idServico);
            return PartialView(listaDias);
        }

        [HttpGet]
        public ActionResult AgendarServicoHoras(int idServico, DateTime data,
                                                string nomeDia, string nomeOrgao,
                                                string nomeServico, int idOrgao,
                                                string iconeServico, int idArea)
        {
            ViewBag.nomeOrgaoPublico = nomeOrgao;
            ViewBag.nomeServicoPublico = nomeServico;
            ViewBag.nomeDia = nomeDia;
            ViewBag.idOrgao = idOrgao;
            ViewBag.iconeServico = iconeServico;
            ViewBag.idServico = idServico;
            ViewBag.idArea = idArea;
            var listaHoras = _diaAgendamentoService.GetAllHorasByIdServicoAndDia(idServico, data);
            return PartialView(listaHoras);
        }

        [HttpGet]
        public ActionResult ConfirmarAgendamento(int idDiaAgendamento)
        {
            SetLayout();
            var dadosAgendamento = _diaAgendamentoService.GetDadosAgendamento(idDiaAgendamento);
            ViewBag.idServico = dadosAgendamento.IdServico;
            ViewBag.data = dadosAgendamento.Data;
            ViewBag.nomeDia = dadosAgendamento.NomeDia;
            ViewBag.nomeServico = dadosAgendamento.NomeServico;
            ViewBag.iconeServico = dadosAgendamento.IconeServico;
            ViewBag.idOrgao = dadosAgendamento.IdOrgao;
            ViewBag.nomeOrgao = dadosAgendamento.NomeOrgao;
            ViewBag.idArea = dadosAgendamento.IdArea;

            var cookie = Request.Cookies.FirstOrDefault(c => c.Key == "AgendeMeSession");
            if (cookie.Value == null) 
            {
                ViewBag.logado = false;
            }else 
            {
                ViewBag.logado = true;
            }

            return View(dadosAgendamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarAgendamento(AgendarServicoViewModel agendamentoModel)
        {
            try
            {
                var cookie = Request.Cookies.FirstOrDefault(c => c.Key == "AgendeMeSession");
                if (cookie.Value == null) 
                {
                    return RedirectToAction(nameof(ConfirmarAgendamento), new { idDiaAgendamento = agendamentoModel.IdDiaAgendamento });
                }

                var agendamento = _mapper.Map<Agendamento>(agendamentoModel);
                agendamento.IdCidadao = Convert.ToInt32(User.FindFirst("Id")?.Value);
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
        public ActionResult GetSession(string CPF)
        {
            var cookie = Request.Cookies.FirstOrDefault(c => c.Key == "AgendeMeSession");
            if (cookie.Value == null) 
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet]
        public ActionResult AgendamentoConfirmado(int id)
        {
            SetLayout();
            AgendamentoDTO agendamento = _agendamentoService.GetDados(id);
            return View(agendamento);
        }

        public ActionResult Atendimento(int? idServico, int? idOrgao)
        {
            IEnumerable<Orgaopublico> orgaos;
            List<Servicopublico> servicos = new ();
            if (User.IsInRole(Papeis.Atendente)) 
            {
                idOrgao = Convert.ToInt32(User.FindFirst("IdOrgao")?.Value);
                var orgao = _orgaoPublicoService.Get((int)idOrgao);
                orgaos = new [] { orgao };
                servicos = _servicoPublicoService.GetAllByIdOrgao((int)idOrgao).ToList();

                if (idOrgao != null && idServico != null) 
                {
                    if (_diaAgendamentoService.GetAllHorasByIdServicoAndDia((int)idServico, new DateTime(2024, 02, 26)).Any()) 
                    {
                        ViewData["idServico"] = idServico;
                    }
                }

            }
            else
            {
                orgaos = _orgaoPublicoService.GetAll();
                if (idOrgao != null) 
                {
                    servicos = _servicoPublicoService.GetAllByIdOrgao((int)idOrgao).ToList();
                    if (idServico != null) 
                    {
                        if (_diaAgendamentoService.GetAllHorasByIdServicoAndDia((int)idServico, new DateTime(2024, 02, 26)).Any()) 
                        {
                            ViewData["idServico"] = idServico;
                        }
                    }
                }
            }
            
            SetLayout();
            return View(new Atendimento {
                ListaOrgaos = new SelectList(orgaos,"Id", "Nome", null),
                ListaServicos = new SelectList(servicos,"Id", "Nome", null)
                });
        }

        public ActionResult PainelAtendimento(int id) 
        {
            return View();
        }

        public ActionResult GetAtendimentos(int id) 
        {
            return PartialView("~/Views/AgendarServico/_PainelAtendimento.cshtml", _agendamentoService.GetAtendimentos(id));
        }
    }
}
