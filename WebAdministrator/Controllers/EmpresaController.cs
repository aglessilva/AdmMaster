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
    public class EmpresaController : Controller
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetAutoComplete(string _campo, int _type = 0)
        {
            using DbCon db = new DbCon();

            if (_type == 0)
            {
                using var contexto = new RepositoryGeneric<Empresa>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll(n => n.No_Empresa.ToUpper().StartsWith(_campo.ToUpper()));
                    var query = consulta.Select(n => new { text = n.No_Empresa.ToString(), id = n.Cd_Empresa.ToString() });

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
            else
            {
                using var contexto = new RepositoryGeneric<Empresa_Envolvida_Origem>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll(n => n.No_Empresa.ToUpper().StartsWith(_campo.ToUpper()));
                    var query = consulta.Select(n => new { text = n.No_Empresa.ToString(), id = n.Cd_Empresa.ToString() });

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
        }

        public ActionResult Create(int id)
        {
            try
            {
                if (id > 0)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Empresa>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _empresa = contexto.GetItem(n => n.Cd_Empresa == id);
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
        public ActionResult Create(Empresa _empresa)
        {
            if (_empresa.Cd_Empresa == 0)
                ModelState.Remove("Cd_Empresa");

            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Empresa>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        _empresa.No_Empresa = _empresa.No_Empresa.ToUpper();
                        _empresa.Nu_Cnpj = Regex.Replace(_empresa.Nu_Cnpj, @"[^0-9$]", string.Empty);
                        _empresa.Cd_UsuarioCriacao = _empresa.Cd_UsuarioAtualizacao = User.Identity.Name ?? "anonymous";
                        _empresa.Dt_Criacao = _empresa.Dt_Atualizacao = DateTime.Now.Date;
                        int ret = contexto.Create(_empresa);
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
        public ActionResult Edit(Empresa _empresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Empresa>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        _empresa.Nu_Cnpj = Regex.Replace(_empresa.Nu_Cnpj, @"[^0-9$]", string.Empty);
                        _empresa.Cd_UsuarioAtualizacao = User.Identity.Name ?? "ANONYMOUS";
                        _empresa.Dt_Atualizacao = DateTime.Now.Date;

                        int ret = contexto.Edit(_empresa);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
                    }
                    else   
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                    return View("Create", _empresa);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("/Empresa/Search/{skip:int}")]
        public PartialViewResult Search(string empresa, int skip = 1)
        {
            try
            {
                Empresa obj = JsonConvert.DeserializeObject<Empresa>(empresa);

                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);
                obj.Nu_Cnpj = Regex.Replace(obj.Nu_Cnpj, @"[^0-9$]", string.Empty);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<Empresa>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll
                        (
                            x => (x.No_Empresa.ToUpper().StartsWith(obj.No_Empresa.ToUpper()) && x.Nu_Cnpj.StartsWith(obj.Nu_Cnpj))
                             && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status)
                        ).OrderBy(i => i.No_Empresa);

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
