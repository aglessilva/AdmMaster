using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using WebAdministrator.DAL;
using WebAdministrator.Models;

namespace WebAdministrator.Controllers
{
    public class StartController : Controller
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Usuario>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _Usuario = contexto.GetItem(n => n.Login == login.UserName && n.Password == Util.Utility.Criptografar(login.Password) && n.Status);
                        if (_Usuario != null)
                        {
                            var token = Util.Utility.GenerateToken(_Usuario);
                            HttpContext.Session.SetString("Token", token);
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                        else
                        {
                            ViewBag.invalido = true;
                            return View(login);
                        }
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else

                    return View();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Start");
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }
    }
}
