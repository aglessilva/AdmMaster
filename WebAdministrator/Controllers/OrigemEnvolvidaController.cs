using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAdministrator.DAL;
using WebAdministrator.Models;
using X.PagedList;

namespace WebAdministrator.Controllers
{
    [Authorize]
    public class OrigemEnvolvidaController : Controller
    {
        GenericRepositoryValidation.GenericRepositoryExceptionStatus status;


        // GET: OrigemEnvolvidaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrigemEnvolvidaController/Create
        public ActionResult Create(int id)
        {
            try
            {
                if (id > 0)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<OrigemEnvolvida>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _centroCurso = contexto.GetItem(n => n.Cd_Centro_Custo == id);
                        _centroCurso.Ds_Empresa_Origem = db.Empresa.FirstOrDefault(f => f.Cd_Empresa == _centroCurso.Cd_Empresa_Origem).No_Empresa;
                        _centroCurso.Ds_Empresa_Envolvida = db.Empresa_Envolvida.FirstOrDefault(f => f.Cd_Empresa == _centroCurso.Cd_Empresa_Envolvida).No_Empresa;

                        return View(_centroCurso);
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

        // POST: OrigemEnvolvidaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrigemEnvolvida OrigemEnvolvida)
        {
            try
            {
                ModelState.Remove("Cd_Centro_Custo");

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<OrigemEnvolvida>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        OrigemEnvolvida.Cd_Usuario_Criacao = OrigemEnvolvida.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        OrigemEnvolvida.Dt_Criacao = OrigemEnvolvida.Dt_Atualizacao;
                        int ret = contexto.Create(OrigemEnvolvida);
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

        // POST: OrigemEnvolvidaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrigemEnvolvida OrigemEnvolvida)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<OrigemEnvolvida>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        OrigemEnvolvida.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        OrigemEnvolvida.Dt_Criacao = OrigemEnvolvida.Dt_Atualizacao;
                        int ret = contexto.Edit(OrigemEnvolvida);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
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

        [HttpGet]
        [Route("/OrigemEnvolvida/Search/{skip:int}")]
        public PartialViewResult Search(string origemenvolvida, int skip = 1)
        {
            try
            {
                OrigemEnvolvida obj = JsonConvert.DeserializeObject<OrigemEnvolvida>(origemenvolvida);
                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<OrigemEnvolvida>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var consulta = contexto.GetAll
                        (
                            x => (x.Cd_Empresa_Envolvida == (obj.Cd_Empresa_Envolvida?? x.Cd_Empresa_Envolvida))
                                && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status)
                        );

                    var empresa_envolvida = db.Empresa_Envolvida.ToList();

                    List<OrigemEnvolvida> listagem = consulta.ToList();

                    listagem.ForEach(f => {
                        f.Ds_Empresa_Envolvida = empresa_envolvida.FirstOrDefault(n => n.Cd_Empresa == f.Cd_Empresa_Envolvida).No_Empresa;
                        f.Ds_Empresa_Origem = empresa_envolvida.FirstOrDefault(n => n.Cd_Empresa == f.Cd_Empresa_Origem).No_Empresa;
                    });

                    var query = listagem.OrderBy(s => s.Ds_Empresa_Envolvida).ToPagedList(skip, 25);
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
