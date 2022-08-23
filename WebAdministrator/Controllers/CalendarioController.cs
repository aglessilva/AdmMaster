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
    public class CalendarioController : Controller
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
                    using var contexto = new RepositoryGeneric<CalendarioAjustavel>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _calendario = contexto.GetItem(n => n.Cd_Calendario == id);
                        return View(_calendario);
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
        public ActionResult Create(CalendarioAjustavel calendarioAjustavel)
        {
            try
            {
                ModelState.Remove("Cd_Calendario");

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<CalendarioAjustavel>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        calendarioAjustavel.Cd_Usuario_Criacao = calendarioAjustavel.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        calendarioAjustavel.Dt_Criacao = calendarioAjustavel.Dt_Atualizacao;
                        int ret = contexto.Create(calendarioAjustavel);
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
        public ActionResult Edit(CalendarioAjustavel calendarioAjustavel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<CalendarioAjustavel>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        calendarioAjustavel.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";

                        int ret = contexto.Edit(calendarioAjustavel);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                    return View("Create", calendarioAjustavel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("/Calendario/Search/{skip:int}")]
        public PartialViewResult Search(string calendario, int skip = 1)
        {
            try
            {
                CalendarioAjustavel obj = JsonConvert.DeserializeObject<CalendarioAjustavel>(calendario);
                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<CalendarioAjustavel>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll
                        (
                            x => (x.Ds_Titulo_Data.ToUpper().StartsWith(obj.Ds_Titulo_Data.ToUpper()))
                                && (x.Dt_Nacional.StartsWith(obj.Dt_Nacional))
                                && (x.Sg_Uf == (string.IsNullOrWhiteSpace(obj.Sg_Uf) ? x.Sg_Uf: obj.Sg_Uf))
                                && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status)
                        ).OrderBy(i => i.Ds_Titulo_Data);

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
