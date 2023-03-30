using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PW_Proyecto.Models;

namespace PW_Proyecto.Controllers
{
    public class UserController : Controller
    {
        private readonly TorneoappContext appContext = new TorneoappContext();

        // GET: UserController
        public async Task<IActionResult> Index()
        {
            return View(await appContext.Users.ToListAsync());
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Models.Torneo user = appContext.Users.Find(id);
                return View(user);
            }
            catch (Exception)
            {

                return View();
            }
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Torneo newUser)
        {
            try
            {
                appContext.Users.Add(newUser);
                appContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Models.Torneo usuario = appContext.Users.FindAsync(id).Result;
                if (usuario == null) {
                    return NotFound();
                }
                return View(usuario);
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Models.Torneo editUser)
        {
            try
            {
                appContext.Users.Update(editUser);
                appContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            Models.Torneo user = appContext.Users.Find(id);
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Torneo user)
        {
            try
            {
                appContext.Users.Remove(user);
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
