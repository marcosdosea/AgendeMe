using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers
{
    public class AreaDeServicoController : Controller
    {
        // GET: AreaDeServicoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AreaDeServicoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AreaDeServicoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaDeServicoController/Create
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

        // GET: AreaDeServicoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AreaDeServicoController/Edit/5
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

        // GET: AreaDeServicoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AreaDeServicoController/Delete/5
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
