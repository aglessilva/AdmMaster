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
    public class MotivoAvaliacaoController : Controller
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
                    using var contexto = new RepositoryGeneric<MotivoAvaliacao>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _motivo = contexto.GetItem(n => n.Cd_Motivo_Avaliacao == id);
                        return View(_motivo);
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
        public ActionResult Create(MotivoAvaliacao motivoAvaliacao)
        {
            if (motivoAvaliacao.Cd_Motivo_Avaliacao == 0)
                ModelState.Remove("Cd_Motivo_Avaliacao");

            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<MotivoAvaliacao>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        motivoAvaliacao.Cd_Usuario_Criacao = motivoAvaliacao.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        motivoAvaliacao.Dt_Criacao = motivoAvaliacao.Dt_Atualizacao = DateTime.Now.Date;
                        int ret = contexto.Create(motivoAvaliacao);
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
        public ActionResult Edit(MotivoAvaliacao _motivoAvaliacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<MotivoAvaliacao>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        _motivoAvaliacao.Cd_Usuario_Atualizacao= User.Identity.Name ?? "ANONYMOUS";
                        _motivoAvaliacao.Dt_Atualizacao = DateTime.Now.Date;

                        int ret = contexto.Edit(_motivoAvaliacao);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                    return View("Create", _motivoAvaliacao);
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
            using var contexto = new RepositoryGeneric<MotivoAvaliacao>(db, out status);
            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
            {
                var consulta = contexto.GetAll(n => n.Ds_Motivo.ToUpper().StartsWith(_campo.ToUpper()));
                var query = consulta.Select(n => new { text = n.Ds_Motivo.ToString(), id = n.Cd_Motivo_Avaliacao.ToString() });

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
        [Route("/MotivoAvaliacao/Search/{skip:int}")]
        public PartialViewResult Search(string motivo, int skip = 1)
        {
            try
            {
                MotivoAvaliacao obj = JsonConvert.DeserializeObject<MotivoAvaliacao>(motivo);
                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<MotivoAvaliacao>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll
                        (
                            x => (x.Ds_Motivo.ToUpper().StartsWith(obj.Ds_Motivo.ToUpper())) && (x.Tipo == (obj.Tipo == 0 ? x.Tipo : obj.Tipo))
                             && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status)
                        ).OrderBy(i => i.Ds_Motivo);

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
