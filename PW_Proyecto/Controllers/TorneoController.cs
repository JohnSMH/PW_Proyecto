using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PW_Proyecto.Models;

namespace PW_Proyecto.Controllers
{
    public class TorneoController : Controller
    {
        private readonly TorneoappContext appContext = new TorneoappContext();
        // GET: TorneoController
        public async Task<IActionResult> Index()
        {
            return View(await appContext.Torneos.ToListAsync());
        }

        // GET: TorneoController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Torneo user = appContext.Torneos.Find(id);
                return View(user);
            }
            catch (Exception)
            {

                return View();
            }
        }

        // GET: TorneoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TorneoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Torneo newTorneo)
        {
            try
            {
                appContext.Torneos.Add(newTorneo);
                appContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TorneoController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Torneo torneo = appContext.Torneos.FindAsync(id).Result;
                if (torneo == null)
                {
                    return NotFound();
                }
                return View(torneo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: TorneoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Torneo torneo)
        {
            try
            {
                appContext.Torneos.Update(torneo);
                appContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TorneoController/Delete/5
        public ActionResult Delete(int id)
        {
            Torneo torneo = appContext.Torneos.Find(id);
            return View(torneo);
        }

        // POST: TorneoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Torneo torneo)
        {
            try
            {
                appContext.Torneos.Remove(torneo);
                appContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
