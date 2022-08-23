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
    public class ComarcaController : Controller
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
                    using var contexto = new RepositoryGeneric<Comarca>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _empresa = contexto.GetItem(n => n.Cd_Comarca == id);
                        return View(_empresa);
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
        public ActionResult Create(Comarca _comarca)
        {
            try
            {
                if (_comarca.Cd_Comarca == 0)
                    ModelState.Remove("Cd_Comarca");

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Comarca>(db, out status);
                    
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        _comarca.No_Comarca = _comarca.No_Comarca.ToUpper();
                        _comarca.Dt_Criacao = _comarca.Dt_Atualizacao;
                        _comarca.Cd_Usuario_Criacao = _comarca.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";

                        int ret = contexto.Create(_comarca);
                        ModelState.Clear();
                        ViewData["retorno"] = ret;
                        return View();
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
        public ActionResult Edit(Comarca _comarca)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Comarca>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var comarca =  contexto.GetItem(n => n.Cd_Comarca == _comarca.Cd_Comarca);

                        comarca.No_Comarca = _comarca.No_Comarca.ToUpper();
                        comarca.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        comarca.Status = _comarca.Status;
                        comarca.Sg_Uf = _comarca.Sg_Uf;

                        int ret = contexto.Edit(comarca);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                    return View("Create",_comarca);
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
            using var contexto = new RepositoryGeneric<Comarca>(db, out status);
            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
            {
                var consulta = contexto.GetAll(n => n.No_Comarca.ToUpper().StartsWith(_campo.ToUpper()));
                var query = consulta.Select(n => new { text = n.No_Comarca.ToString(), id = n.No_Comarca.ToString() });

                if (query == null)
                    return null;
                else
                    return JsonConvert.SerializeObject(query, Formatting.None);
            }
            else
                return "";
        }

        [HttpGet]
        [Route("/Comarca/Search/{skip:int}")]
        public PartialViewResult Search(string comarca, int skip = 1)
        {
            try
            {
                Comarca obj = JsonConvert.DeserializeObject<Comarca>(comarca);
                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<Comarca>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll
                        (
                            x => (obj.Sg_Uf.Length > 1 ? x.Sg_Uf == obj.Sg_Uf: x.Cd_Comarca== x.Cd_Comarca) && (x.No_Comarca.ToUpper().StartsWith(obj.No_Comarca.ToUpper()) && x.No_Comarca.ToUpper().StartsWith(obj.No_Comarca.ToUpper()))
                             && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status)
                        ).OrderBy(n => n.No_Comarca);

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
