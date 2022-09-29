using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers
{
    public class PrefeituraController : Controller
    {
        private readonly IPrefeituraService _prefeituraService;
        private readonly IMapper _mapper;

        public PrefeituraController(IPrefeituraService prefeituraService, IMapper mapper)
        {
            _prefeituraService = prefeituraService;
            _mapper = mapper;
        }

        // GET: PrefeituraController
        public ActionResult Index()
        {
            var listaPrefeituras = _prefeituraService.GetAll();
            var listaPrefeituraModel = _mapper.Map<List<PrefeituraViewModel>>(listaPrefeituras);
            return View(listaPrefeituraModel);
        }

        // GET: PrefeituraController/Details/5
        public ActionResult Details(int id)
        {
            Prefeitura prefeitura = _prefeituraService.Get(id);
            PrefeituraViewModel prefeituraModel = _mapper.Map<PrefeituraViewModel>(prefeitura);
            return View(prefeituraModel);
        }

        // GET: PrefeituraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrefeituraController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrefeituraViewModel prefeituraModel)
        {
            if (ModelState.IsValid)
            {
                var prefeitura = _mapper.Map<Prefeitura>(prefeituraModel);
                _prefeituraService.Create(prefeitura);
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: PrefeituraController/Edit/5
        public ActionResult Edit(int id)
        {
            Prefeitura prefeitura = _prefeituraService.Get(id);
            PrefeituraViewModel prefeituraModel = _mapper.Map<PrefeituraViewModel>(prefeitura);
            return View(prefeituraModel);
        }

        // POST: PrefeituraController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PrefeituraViewModel prefeituraModel)
        {
            if (ModelState.IsValid)
            {
                var prefeitura = _mapper.Map<Prefeitura>(prefeituraModel);
                _prefeituraService.Edit(prefeitura);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: PrefeituraController/Delete/5
        public ActionResult Delete(int id)
        {
            Prefeitura prefeitura = _prefeituraService.Get(id);
            PrefeituraViewModel prefeituraModel = _mapper.Map<PrefeituraViewModel>(prefeitura);
            return View(prefeituraModel);
        }

        // POST: PrefeituraController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, PrefeituraViewModel prefeituraModel)
        {
            _prefeituraService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
