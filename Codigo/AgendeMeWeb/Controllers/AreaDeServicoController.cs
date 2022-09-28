using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers
{
    public class AreaDeServicoController : Controller
    {
        private readonly IAreaDeServicoService _areaDeServicoService;
        private readonly IMapper _mapper;

        public AreaDeServicoController(IAreaDeServicoService areaDeServicoService, IMapper mapper)
        {
            _areaDeServicoService = areaDeServicoService;
            _mapper = mapper;
        }

        // GET: AreaDeServicoController
        public ActionResult Index()
        {
            var listaAreasDeServico = _areaDeServicoService.GetAll();
            var listaAreasDeServicoModel = _mapper.Map<List<AreaDeServicoViewModel>>(listaAreasDeServico);
            return View(listaAreasDeServicoModel);
        }

        // GET: AreaDeServicoController/Details/5
        public ActionResult Details(int id)
        {
            Areadeservico areaDeServico = _areaDeServicoService.Get(id);
            AreaDeServicoViewModel areaDeServicoModel = _mapper.Map<AreaDeServicoViewModel>(areaDeServico);
            return View(areaDeServicoModel);
        }

        // GET: AreaDeServicoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaDeServicoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AreaDeServicoViewModel areaDeServicoModel)
        {
            try
            {
                var areaDeServico = _mapper.Map<Areadeservico>(areaDeServicoModel);
                _areaDeServicoService.Create(areaDeServico);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AreaDeServicoController/Edit/5
        public ActionResult Edit(int id)
        {
            Areadeservico areaDeServico = _areaDeServicoService.Get(id);
            AreaDeServicoViewModel areaDeServicoModel = _mapper.Map<AreaDeServicoViewModel>(areaDeServico);
            return View(areaDeServicoModel);
        }

        // POST: AreaDeServicoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AreaDeServicoViewModel areaDeServicoModel)
        {
            try
            {
                var areaDeServico = _mapper.Map<Areadeservico>(areaDeServicoModel);
                _areaDeServicoService.Edit(areaDeServico);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AreaDeServicoController/Delete/5
        public ActionResult Delete(int id)
        {
            Areadeservico areaDeServico = _areaDeServicoService.Get(id);
            AreaDeServicoViewModel areaDeServicoModel = _mapper.Map<AreaDeServicoViewModel>(areaDeServico);
            return View(areaDeServicoModel);
        }

        // POST: AreaDeServicoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AreaDeServicoViewModel areaDeServicoModel)
        {
            try
            {
                _areaDeServicoService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
