using AgendeMeWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers
{
    public class PrefeituraController : Controller
    {
        // GET: PrefeituraController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PrefeituraController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrefeituraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrefeituraController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrefeituraController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrefeituraController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PrefeituraController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrefeituraController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
