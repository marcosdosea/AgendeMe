using AgendeMeWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers
{
    public class CargoController : Controller
    {
        private readonly ICargoService _cargoService;
        private readonly IMapper _mapper;

        public CargoController(ICargoService cargoService, IMapper mapper)
        {
            _cargoService = cargoService;
            _mapper = mapper;
        }

        // GET: CargoController
        public ActionResult Index()
        {
            var listaCargos = _cargoService.GetAll();
            var listaCargosModel = _mapper.Map<List<CargoViewModel>>(listaCargos);
            return View(listaCargosModel);
        }

        // GET: CargoController/Details/5
        public ActionResult Details(int id)
        {
            Cargo cargo = _cargoService.Get(id);
            CargoViewModel cargoModel = _mapper.Map<CargoViewModel>(cargo);
            return View(cargoModel);
        }

        // GET: CargoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CargoViewModel cargoModel)
        {
            if (ModelState.IsValid)
            {
                var cargo = _mapper.Map<Cargo>(cargoModel);
                _cargoService.Create(cargo);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CargoController/Edit/5
        public ActionResult Edit(int id)
        {
            Cargo cargo = _cargoService.Get(id);
            CargoViewModel cargoModel = _mapper.Map<CargoViewModel>(cargo);
            return View(cargoModel);
        }

        // POST: CargoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CargoViewModel cargoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cargo = _mapper.Map<Cargo>(cargoModel);
                    _cargoService.Edit(cargo);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CargoController/Delete/5
        public ActionResult Delete(int id)
        {
            Cargo cargo = _cargoService.Get(id);
            CargoViewModel cargoModel = _mapper.Map<CargoViewModel>(cargo);
            return View(cargoModel);
        }

        // POST: CargoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CargoViewModel cargoModel)
        {
            try
            {
                _cargoService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
