#pragma checksum "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e686308f3eee428749d5146d33531158b08bef61"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Pedido_ListParecer), @"mvc.1.0.view", @"/Views/Pedido/ListParecer.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Projetos\WebAdministrator\WebAdministrator\Views\_ViewImports.cshtml"
using WebAdministrator;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Projetos\WebAdministrator\WebAdministrator\Views\_ViewImports.cshtml"
using WebAdministrator.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e686308f3eee428749d5146d33531158b08bef61", @"/Views/Pedido/ListParecer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a33317e823cb4a7ff87e51c1e68d602fcd4037de", @"/Views/_ViewImports.cshtml")]
    public class Views_Pedido_ListParecer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<X.PagedList.IPagedList<dynamic>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml"
 if (Model?.Count() > 0)
{
    var listItem = new SelectList(WebAdministrator.Util.Utility.GetSelectLists<Sentenca>(x => x.Cd_Sentenca > 0).Select(y => new SelectListItem { Value = y.Cd_Sentenca.ToString(), Text = y.Ds_Sentenca }).AsEnumerable(), "Value", "Text");
    if (Model is X.PagedList.IPagedList<Justificativa>)
    {


#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <table class=""table"" style=""margin:auto; width:98%;"">
            <thead>
                <tr>
                    <th style=""width:100%;"">
                        Parecer
                    </th>
                    <th style=""width:200px;"" class=""left-align"">
                        Tipo de Sentença
                    </th>
                    <th style=""width:120px;"" class=""left-align"">
                        Status
                    </th>
                    <th style=""width:150px;"" class=""totalitemcount"">Total de Registros: ");
#nullable restore
#line 21 "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml"
                                                                                   Write(Model.TotalItemCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
#nullable restore
#line 25 "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml"
                 foreach (Justificativa item in Model)
                {
                    if (item != null)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td style=\"width:100%;\">\r\n                        ");
#nullable restore
#line 31 "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Ds_Justificativa_Sentenca));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td style=\"width:200px;\" class=\"left-align\">\r\n                        ");
#nullable restore
#line 34 "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml"
                   Write(Html.DisplayFor(modelItem => listItem.FirstOrDefault(f => f.Value == item.Cd_Sentenca.ToString()).Text));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td style=\"width:100px;\" class=\"left-align\">\r\n                        ");
#nullable restore
#line 37 "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml"
                    Write(item.Status ? "Ativo" : "Inativo");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td style=\"width:150px;\" class=\"right-align\">\r\n                        <a");
            BeginWriteAttribute("data", " data=\"", 1820, "\"", 1860, 1);
#nullable restore
#line 40 "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml"
WriteAttributeValue("", 1827, Json.Serialize(@item).ToString(), 1827, 33, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" onclick=\"JavaScript: EditParecer(this);\" class=\"btn-small waves-effect waves-light orange darken-3\">Editar</a>\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 43 "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml"
                    }
                    else
                    { break; }
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n");
#nullable restore
#line 49 "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml"
    }
    if (Model is X.PagedList.IPagedList<Microsoft.AspNetCore.Mvc.ModelBinding.ModelError>)
    {

        foreach (Microsoft.AspNetCore.Mvc.ModelBinding.ModelError item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <label data=\"0\" >");
#nullable restore
#line 55 "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml"
                        Write(item.ErrorMessage.Trim());

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n");
#nullable restore
#line 56 "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml"
        }
    }
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""col row z-depth-3 red white-text center-align"" style=""height: 50px; width: 95% ;border: 1px solid; border-radius: 10px; padding:12px 0px 0px 0px;"">
        <div class=""col s1 left-align""><i class=""material-icons white-text"">report_problem</i></div>
        <div class=""col s11 center"">Nenhuma registro foi localizado com o filtro aplicado</div>
    </div>
");
#nullable restore
#line 65 "C:\Projetos\WebAdministrator\WebAdministrator\Views\Pedido\ListParecer.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<X.PagedList.IPagedList<dynamic>> Html { get; private set; }
    }
}
#pragma warning restore 1591
