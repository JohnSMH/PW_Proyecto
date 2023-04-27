using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PW_Proyecto.Models;

namespace PW_Proyecto.Controllers
{
    public class PartidosController : Controller
    {
        // GET: PartidosController
        public ActionResult Index()
        {
            return View(Functions.APIServicePartidos.GetPartidos().Result);
        }

        public ActionResult IndexByTorneo(int id)
        {
            return View(Functions.APIServicePartidos.GetPartidosFilterTorneo(id).Result);
        }

        // GET: PartidosController/Details/5
        public ActionResult Details(int id)
        {
            Partido partido = Functions.APIServicePartidos.GetPartido(id).Result;
            return View(partido);
        }


        // GET: PartidosController/Edit/5
        public ActionResult Edit(int id)
        {
            Partido partido = Functions.APIServicePartidos.GetPartido(id).Result;
            return View(partido);
        }

        // POST: PartidosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Partido partido)
        {
            try
            {
                Functions.APIServicePartidos.PutPartido(partido, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
       
    }
}
