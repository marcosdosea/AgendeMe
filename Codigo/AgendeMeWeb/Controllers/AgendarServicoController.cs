using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers
{
    public class AgendarServicoController : Controller
    {
        // GET: AgendarServicoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AgendarServicoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AgendarServicoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgendarServicoController/Create
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

        // GET: AgendarServicoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AgendarServicoController/Edit/5
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

        // GET: AgendarServicoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AgendarServicoController/Delete/5
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
