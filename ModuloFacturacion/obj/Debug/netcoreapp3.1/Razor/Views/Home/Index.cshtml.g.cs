#pragma checksum "C:\Users\SISTEMAS4\source\repos\ModuloFacturacion\ModuloFacturacion\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d8713257b033effdddf5d8c1f4117c17898f8fd7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "C:\Users\SISTEMAS4\source\repos\ModuloFacturacion\ModuloFacturacion\Views\_ViewImports.cshtml"
using ModuloFacturacion;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\SISTEMAS4\source\repos\ModuloFacturacion\ModuloFacturacion\Views\_ViewImports.cshtml"
using ModuloFacturacion.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d8713257b033effdddf5d8c1f4117c17898f8fd7", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0295f722ffac61f8744d901c0662fc51930e2152", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ModuloFacturacion.Models.Factura>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h4>Lista de facturas</h4>\r\n<table class=\"table table-hover\">\r\n    <tr>\r\n        <th>Nro Factura</th>\r\n        <th>Cliente</th>\r\n        <th>Fecha Creacion</th>\r\n        <th>Valor Total</th>\r\n        <th>Acciones</th>\r\n    </tr>\r\n");
#nullable restore
#line 11 "C:\Users\SISTEMAS4\source\repos\ModuloFacturacion\ModuloFacturacion\Views\Home\Index.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 14 "C:\Users\SISTEMAS4\source\repos\ModuloFacturacion\ModuloFacturacion\Views\Home\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.IdFactura));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 15 "C:\Users\SISTEMAS4\source\repos\ModuloFacturacion\ModuloFacturacion\Views\Home\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.IdClienteNavigation.NombreCliente));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 16 "C:\Users\SISTEMAS4\source\repos\ModuloFacturacion\ModuloFacturacion\Views\Home\Index.cshtml"
           Write(string.Format("{0:d}", item.FechaCreacion));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 17 "C:\Users\SISTEMAS4\source\repos\ModuloFacturacion\ModuloFacturacion\Views\Home\Index.cshtml"
           Write(string.Format("{0:C}", item.ValorTotal));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>\r\n                ");
#nullable restore
#line 19 "C:\Users\SISTEMAS4\source\repos\ModuloFacturacion\ModuloFacturacion\Views\Home\Index.cshtml"
           Write(Html.ActionLink("Detalle", "DetalleFactura"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 22 "C:\Users\SISTEMAS4\source\repos\ModuloFacturacion\ModuloFacturacion\Views\Home\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\r\n        <td colspan=\"5\" align=\"center\">\r\n            ");
#nullable restore
#line 25 "C:\Users\SISTEMAS4\source\repos\ModuloFacturacion\ModuloFacturacion\Views\Home\Index.cshtml"
       Write(Html.ActionLink("Crear", "CrearFactura", "Home", null, new { miAtributo = "Valor1", @class = "btn btn-Primary" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </td>\r\n    </tr>\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ModuloFacturacion.Models.Factura>> Html { get; private set; }
    }
}
#pragma warning restore 1591
