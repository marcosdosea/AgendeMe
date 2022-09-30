using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers
{
    public class ServicoPublicoController : Controller
    {
        private readonly IServicoPublicoService _servicoPublicoService;
        private readonly IMapper _mapper;

        public ServicoPublicoController(IServicoPublicoService servicoPublicoService, IMapper mapper)
        {
            _servicoPublicoService = servicoPublicoService;
            _mapper = mapper;
        }

        // GET: ServicoPublicoController
        public ActionResult Index()
        {
            var listaServicoPublico = _servicoPublicoService.GetAll();
            var listaServicoPublicoModel = _mapper.Map<List<ServicoPublicoViewModel>>(listaServicoPublico);
            return View(listaServicoPublicoModel);
        }

        // GET: ServicoPublicoController/Details/5
        public ActionResult Details(int id)
        {
            Servicopublico servicoPublico = _servicoPublicoService.Get(id);
            ServicoPublicoViewModel listaServicoPublicoModel = _mapper.Map<ServicoPublicoViewModel>(servicoPublico);
            return View(listaServicoPublicoModel);
        }

        // GET: ServicoPublicoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServicoPublicoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServicoPublicoViewModel servicoPublicoModel)
        {
            try
            {
                var servicoPublico = _mapper.Map<Servicopublico>(servicoPublicoModel);
                _servicoPublicoService.Create(servicoPublico);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServicoPublicoController/Edit/5
        public ActionResult Edit(int id)
        {
            Servicopublico servicoPublico = _servicoPublicoService.Get(id);
            ServicoPublicoViewModel listaServicoPublicoModel = _mapper.Map<ServicoPublicoViewModel>(servicoPublico);
            return View(listaServicoPublicoModel);
        }

        // POST: ServicoPublicoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ServicoPublicoViewModel servicoPublicoModel)
        {
            try
            {
                var servicoPublico = _mapper.Map<Servicopublico>(servicoPublicoModel);
                _servicoPublicoService.Edit(servicoPublico);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServicoPublicoController/Delete/5
        public ActionResult Delete(int id)
        {
            Servicopublico servicoPublico = _servicoPublicoService.Get(id);
            ServicoPublicoViewModel listaServicoPublicoModel = _mapper.Map<ServicoPublicoViewModel>(servicoPublico);
            return View(listaServicoPublicoModel);
        }

        // POST: ServicoPublicoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ServicoPublicoViewModel servicoPublicoModel)
        {
            try
            {
                _servicoPublicoService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
