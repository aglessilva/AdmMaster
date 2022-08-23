using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Linq;
using WebAdministrator.DAL;
using WebAdministrator.Models;
using X.PagedList;

namespace WebAdministrator.Controllers
{
    [Authorize]
    public class PedidoController : Controller
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
                    using var contexto = new RepositoryGeneric<Pedido>(db, out status);
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        var _pedido = contexto.GetItem(n => n.Cd_Tipo_Pedido == id);
                        return View(_pedido);
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
        public ActionResult Create(Pedido _pedido)
        {
            try
            {
                if (_pedido.Cd_Tipo_Pedido == 0)
                    ModelState.Remove("Cd_Tipo_Pedido");

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Pedido>(db, out status);
                    
                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        _pedido.Dt_Criacao = _pedido.Dt_Atualizacao;
                        _pedido.Cd_Usuario_Criacao = _pedido.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        int ret = contexto.Create(_pedido);
                        ModelState.Clear();
                        ViewData["retorno"] = ret;
                        return View();
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                {
                    return View();
                }
                 //   return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult AddJustificativa(Justificativa justificativa)
        {
            try
             {
                if(justificativa.Cd_Justificativa_Sentenca == 0)
                    ModelState.Remove("Cd_Justificativa_Sentenca");

                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Justificativa>(db, out status);

                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        if (justificativa.Cd_Justificativa_Sentenca == 0)
                        {
                            justificativa.Dt_Criacao = justificativa.Dt_Atualizacao;
                            justificativa.Cd_Usuario_Criacao = User.Identity.Name ?? "ANONYMOUS";
                            contexto.Create(justificativa);
                        }
                        else
                        {
                            Justificativa _justificativa = contexto.GetItem(x => x.Cd_Justificativa_Sentenca == justificativa.Cd_Justificativa_Sentenca);

                            _justificativa.Status = justificativa.Status;
                            _justificativa.Ds_Justificativa_Sentenca = justificativa.Ds_Justificativa_Sentenca;
                            _justificativa.Cd_Sentenca = justificativa.Cd_Sentenca;
                            _justificativa.Cd_Usuario_Criacao = User.Identity.Name ?? "ANONYMOUS";
                            contexto.Edit(_justificativa);
                        }
                        ModelState.Clear();
                    }
                    return Parecer(justificativa.Cd_Tipo_Pedido);
                }
                else
                {
                    var erros = ModelState.Values.Where(v => v.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid).ToList().Select(f =>f.Errors);
                    var lstErro =  erros.Select(y => y.FirstOrDefault());
                    return PartialView("ListParecer",lstErro.ToPagedList());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pedido _pedido)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using DbCon db = new DbCon();
                    using var contexto = new RepositoryGeneric<Pedido>(db, out status);

                    if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    {
                        _pedido.Dt_Atualizacao = DateTime.Now.Date;
                        _pedido.Cd_Usuario_Atualizacao = User.Identity.Name ?? "ANONYMOUS";
                        int ret = contexto.Edit(_pedido);
                        ModelState.Clear();
                        ViewData["retorno"] = ret == 1 ? 2 : 0;
                        return View("Create");
                    }
                    else
                        throw new Exception($"Erro na conexão de dados Oracle: {status}");
                }
                else
                {
                    return View("Create",_pedido);
                }
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
            using var contexto = new RepositoryGeneric<Pedido>(db, out status);
            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
            {
                var consulta = contexto.GetAll(n => n.Ds_Tipo_Pedido.ToUpper().StartsWith(_campo.ToUpper()));
                var query = consulta.Select(n => new { text = n.Ds_Tipo_Pedido.ToString(), id = n.Cd_Processo.ToString() });

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
        public string GetAutoCompleteJustificativa(string _campo)
        {
            using DbCon db = new DbCon();
            using var contexto = new RepositoryGeneric<Justificativa>(db, out status);
            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
            {
                var consulta = contexto.GetAll(n => n.Ds_Justificativa_Sentenca.ToUpper().StartsWith(_campo.ToUpper()));
                var query = consulta.Select(n => new { text = n.Ds_Justificativa_Sentenca.ToString(), id = n.Cd_Sentenca.ToString() });

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
        [Route("/Pedido/Search/{skip:int}")]
        public PartialViewResult Search(string pedido, int skip = 1)
        {
            try
            {
                Pedido obj = JsonConvert.DeserializeObject<Pedido>(pedido);
                obj.Status = obj.StatusFilter.HasValue && Convert.ToBoolean((int)obj.StatusFilter);

                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<Pedido>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    dynamic Pedido = new ExpandoObject();
                    var consulta = contexto.GetAll
                        (
                            x => (x.Ds_Tipo_Pedido.ToUpper().StartsWith(obj.Ds_Tipo_Pedido.ToUpper()) && (obj.StatusFilter.HasValue ? x.Status == obj.Status : x.Status == x.Status))
                        ).OrderBy(i => i.Ds_Tipo_Pedido);

                    Pedido = consulta.ToPagedList(skip, 25);
                    if (Pedido == null)
                        return null;
                    else
                        return PartialView("ListRecord", Pedido);
                }

                return PartialView("ListRecord", status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("/Pedido/Parecer/{id:int}")]
        public PartialViewResult Parecer(int id)
        {
            try
            {
                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<Justificativa>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    dynamic justificativa = new ExpandoObject();
                    var consulta = contexto.GetAll(x => x.Cd_Tipo_Pedido == id).OrderBy(o => o.Ds_Justificativa_Sentenca);

                    justificativa = consulta.ToPagedList(1, 25);
                    if (justificativa == null)
                        return null;
                    else
                        return PartialView("ListParecer", justificativa);
                }

                return PartialView("ListParecer", status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
