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
    public class AdvogadoController : Controller
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        // GET: AdvogadoController
        public ActionResult Index()
        {
            return View();
        }

        // POST: AdvogadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Advogado advogado)
        {
            try
            {
                if (advogado.Cd_Advogado == 0)
                    ModelState.Remove("Cd_Advogado");

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Advogado>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        advogado.Cd_Usuario_Criacao = advogado.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        advogado.Dt_Criacao = advogado.Dt_Atualizacao = DateTime.Now.Date;
                        advogado.Nu_Oab = advogado.Nu_Oab.ToUpper();
                        advogado.No_Advogado = advogado.No_Advogado.ToUpper();
                        int ret = contexto.Create(advogado);
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
        public ActionResult Create(int id)
        {
            try
            {
                if (id > 0)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Advogado>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _advogado = contexto.GetItem(n => n.Cd_Advogado == id);
                        return View(_advogado);
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
        public ActionResult Edit(Advogado advogado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Advogado>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        advogado.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        advogado.Dt_Atualizacao = DateTime.Now.Date;
                        advogado.Nu_Oab = advogado.Nu_Oab.ToUpper();
                        advogado.No_Advogado = advogado.No_Advogado.ToUpper();

                        int ret = contexto.Edit(advogado);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                    return View("Create", advogado);
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
            using var contexto = new RepositoryGeneric<Advogado>(db, out status);
            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
            {
                var consulta = contexto.GetAll(n => n.Nu_Oab.ToUpper().StartsWith(_campo.ToUpper()));
                var query = consulta.Select(n => new { text = n.Nu_Oab.ToUpper(), id = n.Cd_Advogado.ToString() });

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
        [Route("/Advogado/Search/{skip:int}")]
        public PartialViewResult Search(string advogado, int skip = 1)
        {
            try
            {
                Advogado obj = JsonConvert.DeserializeObject<Advogado>(advogado);
                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<Advogado>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll
                        (
                            x => (x.Nu_Oab.ToUpper().StartsWith(obj.Nu_Oab.ToUpper()))
                            && (x.No_Advogado.ToUpper().StartsWith(obj.No_Advogado.ToUpper())
                            && (obj.Tipo_Advogado > 0 ? x.Tipo_Advogado == obj.Tipo_Advogado : x.Tipo_Advogado == x.Tipo_Advogado)
                            && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status))
                        ).OrderBy(i => i.No_Advogado);

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
