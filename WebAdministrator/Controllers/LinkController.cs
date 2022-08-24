using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using WebAdministrator.DAL;
using WebAdministrator.Models;
using X.PagedList;

namespace WebAdministrator.Controllers
{

    [Authorize]
    public class LinkController : Controller
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
                    using var contexto = new RepositoryGeneric<Link>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _link = contexto.GetItem(n => n.Cd_Sistema == id);
                        return View(_link);
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
        public ActionResult Create(Link _link)
        {
            try
            {
                if (_link.Cd_Sistema == 0)
                    ModelState.Remove("Cd_Sistema");

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Link>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        _link.Cd_Usuario_Criacao = _link.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        _link.Dt_Criacao = _link.Dt_Atualizacao;
                        int ret = contexto.Create(_link);
                        ModelState.Clear();
                        ViewData["retorno"] = ret;
                        return View();
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Link _link)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Link>(db, out status);

                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {

                        _link.Dt_Atualizacao = DateTime.Now.Date;
                        _link.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        
                        int ret = contexto.Edit(_link);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                    return View("Create", _link);
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
            using var contexto = new RepositoryGeneric<Link>(db, out status);
            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
            {
                var consulta = contexto.GetAll(n => n.No_Sistema.ToUpper().StartsWith(_campo.ToUpper()));
                var query = consulta.Select(n => new { text = n.No_Sistema.ToString(), id = n.Cd_Sistema.ToString() });

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
        [Route("/Link/Search/{skip:int}")]
        public PartialViewResult Search(string link, int skip = 1)
        {
            try
            {
                Link obj = JsonConvert.DeserializeObject<Link>(link);
                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<Link>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll
                        (
                            x => (x.No_Sistema.ToUpper().StartsWith(obj.No_Sistema.ToUpper())) && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status)
                        ).OrderBy(n => n.No_Sistema);

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
