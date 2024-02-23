using AgendeMeWeb.Helpers;
using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers
{
    [Authorize(Roles = Papeis.Administrador)]
    public class PrefeituraController : BaseController
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
            SetLayout();
            var listaPrefeituras = _prefeituraService.GetAll();
            var listaPrefeituraModel = _mapper.Map<List<PrefeituraViewModel>>(listaPrefeituras);
            return View(listaPrefeituraModel);
        }

        // GET: PrefeituraController/Details/5
        public ActionResult Details(int id)
        {
            SetLayout();
            Prefeitura prefeitura = _prefeituraService.Get(id);
            PrefeituraViewModel prefeituraModel = _mapper.Map<PrefeituraViewModel>(prefeitura);
            return View(prefeituraModel);
        }

        // GET: PrefeituraController/Create
        public ActionResult Create()
        {
            SetLayout();
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
            SetLayout();
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
            SetLayout();
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
