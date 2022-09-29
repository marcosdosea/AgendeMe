using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers
{
    public class CidadaoController : Controller
    {
        private readonly ICidadaoService _cidadaoService;
        private readonly IMapper _mapper;

        public CidadaoController(ICidadaoService cidadaoService, IMapper mapper)
        {
            _cidadaoService = cidadaoService;
            _mapper = mapper;
        }

        // GET: CidadaoController
        public ActionResult Index()
        {
            var listaCidadaos = _cidadaoService.GetAll();
            var listaCidadaosModel = _mapper.Map<List<CidadaoViewModel>>(listaCidadaos);
            return View(listaCidadaosModel);
        }

        // GET: CidadaoController/Details/5
        public ActionResult Details(int id)
        {
            Cidadao cidadao = _cidadaoService.Get(id);
            CidadaoViewModel cidadaoViewModel = _mapper.Map<CidadaoViewModel>(cidadao);
            return View(cidadaoViewModel);
        }

        // GET: CidadaoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CidadaoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CidadaoViewModel cidadaoViewModel)
        {
            if (ModelState.IsValid)
            {
                var cidadao = _mapper.Map<Cidadao>(cidadaoViewModel);
                _cidadaoService.Create(cidadao);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CidadaoController/Edit/5
        public ActionResult Edit(int id)
        {
            Cidadao cidadao = _cidadaoService.Get(id);
            CidadaoViewModel cidadaoViewModel = _mapper.Map<CidadaoViewModel>(cidadao);
            return View(cidadaoViewModel);
        }

        // POST: CidadaoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CidadaoViewModel cidadaoViewModel)
        {
            if (ModelState.IsValid)
            {
                var cidadao = _mapper.Map<Cidadao>(cidadaoViewModel);
                _cidadaoService.Edit(cidadao);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CidadaoController/Delete/5
        public ActionResult Delete(int id)
        {
            Cidadao cidadao = _cidadaoService.Get(id);
            CidadaoViewModel cidadaoViewModel = _mapper.Map<CidadaoViewModel>(cidadao);
            return View(cidadaoViewModel);
        }

        // POST: CidadaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CidadaoViewModel cidadaoViewModel)
        {
            _cidadaoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
