﻿using Microsoft.AspNetCore.Http;
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
            return View(Functions.APIServicesUsuarios.GetUsuarios().Result);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Models.User user = Functions.APIServicesUsuarios.GetUsuario(id).Result;
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
        public ActionResult Create(Models.User newUser)
        {
            try
            {
                var result = Functions.APIServicesUsuarios.PostUsuario(newUser);
                //appContext.Users.Add(newUser);
                //appContext.SaveChanges();
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
                Models.User usuario = Functions.APIServicesUsuarios.GetUsuario(id).Result;
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
        public ActionResult Edit(int id, Models.User editUser)
        {
            try
            {
                Functions.APIServicesUsuarios.PutUsuario(editUser,id);
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
            Models.User user = Functions.APIServicesUsuarios.GetUsuario(id).Result;
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, User user)
        {
            try
            {
                Functions.APIServicesUsuarios.DeleteUsuario(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
