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
    public class FamiliaController : Controller
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
                    using var contexto = new RepositoryGeneric<Familia>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _familia = contexto.GetItem(n => n.Cd_Grupo_Familia == id);
                        return View(_familia);
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
        public ActionResult Create(Familia _familia)
        {
            try
            {
                if (_familia.Cd_Grupo_Familia == 0)
                    ModelState.Remove("Cd_Grupo_Familia");

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Familia>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        _familia.Ds_Grupo_Familia = _familia.Ds_Grupo_Familia.ToUpper();
                        _familia.Cd_UsuarioCriacao = _familia.Cd_UsuarioAtualizacao = User.Identity.Name ?? "ANONYMOUS";
                        _familia.Dt_Criacao = _familia.Dt_Atualizacao;
                        int ret = contexto.Create(_familia);
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
        public ActionResult Edit(Familia _familia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Familia>(db, out status);

                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        Familia familia = contexto.GetItem(d => d.Cd_Grupo_Familia == _familia.Cd_Grupo_Familia);

                        familia.Ds_Grupo_Familia = _familia.Ds_Grupo_Familia.ToUpper();
                        familia.Status = _familia.Status;
                        familia.Dt_Atualizacao = _familia.Dt_Atualizacao;
                        familia.Cd_UsuarioAtualizacao = User.Identity.Name ?? "ANONYMOUS";

                        int ret = contexto.Edit(familia);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                    return View("Create", _familia);
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
            using var contexto = new RepositoryGeneric<Familia>(db, out status);
            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
            {
                var consulta = contexto.GetAll(n => n.Ds_Grupo_Familia.ToUpper().StartsWith(_campo.ToUpper()));
                var query = consulta.Select(n => new { text = n.Ds_Grupo_Familia.ToString(), id = n.Cd_Grupo_Familia.ToString() });

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
        [Route("/Familia/Search/{skip:int}")]
        public PartialViewResult Search(string familia, int skip = 1)
        {
            try
            {
                Familia obj = JsonConvert.DeserializeObject<Familia>(familia);
                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<Familia>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll
                        (
                            x => (x.Ds_Grupo_Familia.ToUpper().StartsWith(obj.Ds_Grupo_Familia.ToUpper()) && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status))
                        ).OrderBy(i => i.Ds_Grupo_Familia);

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
