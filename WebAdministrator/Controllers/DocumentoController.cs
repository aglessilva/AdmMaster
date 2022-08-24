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
    public class DocumentoController : Controller
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public ActionResult Index()
        {
            return View();
        }


       // [Authorize(Roles = "BPM")]
        public ActionResult Create(int id)
        {
            try
            {
                if (id > 0)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Documento>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _documento = contexto.GetItem(n => n.Cd_Documento == id);
                        return View(_documento);
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
        public ActionResult Create(Documento _documento)
        {
            try
            {
                if (_documento.Cd_Documento == 0)
                    ModelState.Remove("Cd_documento");

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Documento>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        _documento.Ds_Documento = _documento.Ds_Documento.ToUpper();
                        _documento.Cd_Usuario_Criacao = _documento.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        _documento.Dt_Criacao = _documento.Dt_Atualizacao;
                        int ret = contexto.Create(_documento);
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
        public ActionResult Edit(Documento _documento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Documento>(db, out status);
                    
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        Documento documento = contexto.GetItem(d => d.Cd_Documento == _documento.Cd_Documento);

                        documento.Cd_Tipo_Documento = _documento.Cd_Tipo_Documento;
                        documento.Ds_Documento = _documento.Ds_Documento.ToUpper();
                        documento.Status = _documento.Status;
                        documento.Dt_Atualizacao = _documento.Dt_Atualizacao;
                        documento.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        
                        int ret = contexto.Edit(documento);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                    return View("Create", _documento);
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
            using var contexto = new RepositoryGeneric<Documento>(db, out status);
            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
            {
                var consulta = contexto.GetAll(n => n.Ds_Documento.ToUpper().StartsWith(_campo.ToUpper()));
                var query = consulta.Select(n => new { text = n.Ds_Documento.ToString(), id = n.Cd_Documento.ToString() });

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
        [Route("/Documento/Search/{skip:int}")]
        public PartialViewResult Search(string documento, int skip = 1)
        {
            try
            {
                Documento obj = JsonConvert.DeserializeObject<Documento>(documento);
                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<Documento>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll
                        (
                            x => (obj.Cd_Tipo_Documento > 0 ? x.Cd_Tipo_Documento == obj.Cd_Tipo_Documento : x.Cd_Tipo_Documento == x.Cd_Tipo_Documento) && (x.Ds_Documento.ToUpper().StartsWith(obj.Ds_Documento.ToUpper()))
                             && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status)
                        ).OrderBy(n => n.Ds_Documento);

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
