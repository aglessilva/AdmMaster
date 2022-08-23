using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using WebAdministrator.DAL;
using WebAdministrator.Models;
using X.PagedList;

namespace WebAdministrator.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id)
        {
            try
            {
                if (id > 0)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Usuario>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _usuario = contexto.GetItem(n => n.Cd_Usuario == id);
                        return View(_usuario);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario _usuario)
        {
            try
            {
                ModelState.Remove("Cd_Usuario");

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Usuario>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        _usuario.Cd_Usuario_Criacao = _usuario.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        _usuario.Dt_Criacao = _usuario.Dt_Atualizacao;

                        _usuario.Password = Util.Utility.Criptografar(_usuario.Password);

                        int ret = contexto.Create(_usuario);
                        ModelState.Clear();
                        ViewData["retorno"] = ret;
                        return View();
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario _usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(_usuario.Password))
                {
                    ModelState.Remove("Password");
                    ModelState.Remove("ConfirmPassword");
                }

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Usuario>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var usuario = contexto.GetItem(n => n.Cd_Usuario == _usuario.Cd_Usuario);
                        usuario.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        usuario.Dt_Atualizacao = DateTime.Now.Date;
                        usuario.Status = _usuario.Status;
                        usuario.Email = _usuario.Email;
                        usuario.Login = _usuario.Login;
                        usuario.Nome = _usuario.Nome;
                        usuario.Password = string.IsNullOrWhiteSpace(_usuario.Password) ? usuario.Password : Util.Utility.Criptografar(_usuario.Password);

                        int ret = contexto.Edit(usuario);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                    return View("Create",_usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public string GetAutoComplete(string _campo)
        {
            using DbCon db = new DbCon();
            using var contexto = new RepositoryGeneric<Usuario>(db, out status);
            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
            {
                _campo = Regex.Replace(_campo, @"[^0-9$]", string.Empty);
                var consulta = contexto.GetAll(n => n.Email.ToUpper().StartsWith(_campo.ToUpper()));
                var query = consulta.Select(n => new { text = n.Email , id = n.Cd_Usuario.ToString() });

                if (query == null)
                    return null;
                else
                    return JsonConvert.SerializeObject(query, Formatting.None);
            }
            else
            {
                return "";
            }
        }

        [HttpGet]
        [Route("/Usuario/Search/{skip:int}")]
        public PartialViewResult Search(string usuario, int skip = 1)
        {
            try
            {
                Usuario obj = JsonConvert.DeserializeObject<Usuario>(usuario);

                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<Usuario>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll
                        (
                            x => (x.Nome.ToUpper().StartsWith(obj.Nome.ToUpper()) && x.Login.ToLower().StartsWith(obj.Login.ToLower()) && x.Login.ToLower().StartsWith(obj.Login.ToLower()))
                             && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status)
                        ).OrderBy(i => i.Nome);

                    var query = consulta.ToPagedList(skip, 25);
                    if (query == null)
                        return null;
                    else
                        return PartialView("ListRecord", query);
                }

                return PartialView("ListRecord", status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
