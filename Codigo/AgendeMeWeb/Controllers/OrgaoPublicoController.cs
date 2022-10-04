using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace AgendeMeWeb.Controllers
{
    public class OrgaoPublicoController : Controller
    {

        private readonly IOrgaoPublicoService _orgaoPublicoService;
        private readonly IMapper _mapper;

        public OrgaoPublicoController(IOrgaoPublicoService orgaoPublicoService, IMapper mapper)
        {
            _orgaoPublicoService = orgaoPublicoService;
            _mapper = mapper;
        }

        // GET: OrgaoPublicoController
        public ActionResult Index()
        {
            var listaOrgaosPublicos = _orgaoPublicoService.GetAll();
            var listaOrgaosPublicosModel = _mapper.Map<List<OrgaoPublicoViewModel>>(listaOrgaosPublicos);
            return View(listaOrgaosPublicosModel);
        }

        // GET: OrgaoPublicoController/Details/5
        public ActionResult Details(int id)
        {
            Orgaopublico orgaoPublico = _orgaoPublicoService.Get(id);
            OrgaoPublicoViewModel orgaoPublicoViewModel = _mapper.Map<OrgaoPublicoViewModel>(orgaoPublico);
            return View(orgaoPublicoViewModel);
        }

        // GET: OrgaoPublicoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrgaoPublicoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrgaoPublicoViewModel orgaoPublicoViewModel)
        {
            if (ModelState.IsValid)
            {
                var orgaoPublico = _mapper.Map<Orgaopublico>(orgaoPublicoViewModel);
                _orgaoPublicoService.Create(orgaoPublico);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: OrgaoPublicoController/Edit/5
        public ActionResult Edit(int id)
        {
            Orgaopublico orgaoPublico = _orgaoPublicoService.Get(id);
            OrgaoPublicoViewModel orgaoPublicoViewModel = _mapper.Map<OrgaoPublicoViewModel>(orgaoPublico);
            return View(orgaoPublicoViewModel);
        }

        // POST: OrgaoPublicoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrgaoPublicoViewModel orgaoPublicoViewModel)
        {
            if (ModelState.IsValid)
            {
                var orgaoPublico = _mapper.Map<Orgaopublico>(orgaoPublicoViewModel);
                _orgaoPublicoService.Edit(orgaoPublico);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: OrgaoPublicoController/Delete/5
        public ActionResult Delete(int id)
        {
            Orgaopublico orgaoPublico = _orgaoPublicoService.Get(id);
            OrgaoPublicoViewModel orgaoPublicoViewModel = _mapper.Map<OrgaoPublicoViewModel>(orgaoPublico);
            return View(orgaoPublicoViewModel);
        }

        // POST: OrgaoPublicoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _orgaoPublicoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
