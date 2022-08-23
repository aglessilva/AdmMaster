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
    public class ContratoController : Controller
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato _contrato)
        {
            try
            {
                if (_contrato.Cd_Contrato == 0)
                    ModelState.Remove("Cd_Contrato");

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Contrato>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        _contrato.Ds_Contrato =_contrato.Ds_Contrato.ToUpper();
                        _contrato.Dt_Criacao = _contrato.Dt_Atualizacao;
                        _contrato.Cd_Tipo_Contrato = 1;
                        _contrato.Cd_Usuario_Criacao = _contrato.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        int ret = contexto.Create(_contrato);
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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            try
            {
                if (id > 0)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Contrato>(db, out status);

                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _objeto = contexto.GetItem(n => n.Cd_Contrato == id);
                        return View(_objeto);
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                    return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contrato _contrato)
        {
            try
            {
                if (_contrato.Cd_Contrato == 0)
                    return StatusCode(BadRequest().StatusCode, "O item não foi localizado");

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Contrato>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        Contrato contrato = contexto.GetItem(c => c.Cd_Contrato == _contrato.Cd_Contrato);

                        contrato.Ds_Contrato = _contrato.Ds_Contrato.ToUpper();
                        contrato.Status = _contrato.Status;
                        contrato.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        int ret = contexto.Edit(contrato);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                    return View("Create", _contrato);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public string GetAutoComplete(string _campo)
        {
            using DbCon db = new DbCon();
            using var contexto = new RepositoryGeneric<Contrato>(db, out status);
            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
            {
                var consulta = contexto.GetAll(n => n.Ds_Contrato.ToUpper().StartsWith(_campo.ToUpper()));
                var query = consulta.Select(n => new { text = n.Ds_Contrato.ToString(), id = n.Cd_Contrato.ToString() });

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
        [Route("/Contrato/Search/{skip:int}")]
        public PartialViewResult Search(string contrato, int skip = 1)
        {
            try
            {
                Contrato obj = JsonConvert.DeserializeObject<Contrato>(contrato);

                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<Contrato>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll
                        (
                            x => (x.Ds_Contrato.ToUpper().StartsWith(obj.Ds_Contrato.ToUpper()) && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status))
                        ).OrderBy(n => n.Ds_Contrato);

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
