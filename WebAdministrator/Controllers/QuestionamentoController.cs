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
    public class QuestionamentoController : Controller
    {
        GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

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
                    using var contexto = new RepositoryGeneric<Questionamento>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _Objeto = contexto.GetItem(n => n.Cd_Questao == id);
                        return View(_Objeto);
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
        public ActionResult Create(Questionamento questionamento)
        {
            if (questionamento.Cd_Questao == 0)
                ModelState.Remove("Cd_Questao");

            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Questionamento>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        questionamento.Cd_Usuario_Criacao = questionamento.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        questionamento.Dt_Criacao = questionamento.Dt_Atualizacao = DateTime.Now.Date;
                        int ret = contexto.Create(questionamento);
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
        public ActionResult Edit(Questionamento questionamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Questionamento>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        questionamento.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        questionamento.Dt_Atualizacao = DateTime.Now.Date;

                        int ret = contexto.Edit(questionamento);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                    return View("Create", questionamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("/Questionamento/Search/{skip:int}")]
        public PartialViewResult Search(string questionamento, int skip = 1)
        {
            try
            {
                Questionamento obj = JsonConvert.DeserializeObject<Questionamento>(questionamento);
                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<Questionamento>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll
                        (
                            x => (x.Ds_Questao.ToUpper().StartsWith(obj.Ds_Questao.ToUpper())) 
                                && (x.Cd_Tipo_Contrato == (obj.Cd_Tipo_Contrato == 0 ? x.Cd_Tipo_Contrato: obj.Cd_Tipo_Contrato))
                                && (x.Cd_Tipo_Questionamento == (obj.Cd_Tipo_Questionamento == 0 ? x.Cd_Tipo_Questionamento : obj.Cd_Tipo_Questionamento))
                                && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status)
                        ).OrderBy(i => i.Ds_Questao);

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

        [HttpGet]
        public string GetAutoComplete(string _campo)
        {
            using DbCon db = new DbCon();
            using var contexto = new RepositoryGeneric<Questionamento>(db, out status);
            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
            {
                var consulta = contexto.GetAll(n => n.Ds_Questao.ToUpper().StartsWith(_campo.ToUpper()));
                var query = consulta.Select(n => new { text = n.Ds_Questao.ToString(), id = n.Cd_Questao.ToString() });

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
}
