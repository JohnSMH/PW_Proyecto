using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View(Functions.APIServiceTorneo.GetTorneos().Result);
        }

        // GET: TorneoController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Torneo user = Functions.APIServiceTorneo.GetTorneo(id).Result;
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
            IEnumerable<Models.User> usuarios = Functions.APIServicesUsuarios.GetUsuarios().Result;
            List<SelectListItem> Usuarios = usuarios.Select(info => new SelectListItem
            {
                Value = info.Id.ToString(),
                Text = info.Name.ToString()
            }).ToList();
            ViewBag.Usuarios = Usuarios;
            return View();
        }

        // POST: TorneoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Torneo newTorneo)
        {
            try
            {
                Functions.APIServiceTorneo.PostTorneo(newTorneo);
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
                Torneo torneo = Functions.APIServiceTorneo.GetTorneo(id).Result;
                if (torneo == null)
                {
                    return NotFound();
                }
                IEnumerable<Models.User> usuarios = Functions.APIServicesUsuarios.GetUsuarios().Result;
                List<SelectListItem> Usuarios = usuarios.Select(info => new SelectListItem
                {
                    Value = info.Id.ToString(),
                    Text = info.Name.ToString()
                }).ToList();
                ViewBag.Usuarios = Usuarios;
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
                Functions.APIServiceTorneo.PutTorneo(torneo, id);
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
            Torneo torneo = Functions.APIServiceTorneo.GetTorneo(id).Result;
            return View(torneo);
        }

        // POST: TorneoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Torneo torneo)
        {
            try
            {
                Functions.APIServiceTorneo.DeleteTorneo(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
