#pragma checksum "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\Home\ConfirmSubscribe.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d555667014c1cab03a768ba5694f31f8ce409265"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Ogani.WebUI.Views.Home.Views_Home_ConfirmSubscribe), @"mvc.1.0.view", @"/Views/Home/ConfirmSubscribe.cshtml")]
namespace Ogani.WebUI.Views.Home
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
#line 2 "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\_ViewImports.cshtml"
using Ogani.WebUI.Models.Entity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\_ViewImports.cshtml"
using Ogani.WebUI.Models.ViewModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\_ViewImports.cshtml"
using Ogani.WebUI.AppCode.Types;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\_ViewImports.cshtml"
using Ogani.WebUI.Models.DataContext;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\_ViewImports.cshtml"
using Ogani.WebUI.AppCode.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\_ViewImports.cshtml"
using Resource;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d555667014c1cab03a768ba5694f31f8ce409265", @"/Views/Home/ConfirmSubscribe.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"def64b94be70546facb495209f61e9d90e28660e", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ConfirmSubscribe : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Tuple<string,bool>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\Home\ConfirmSubscribe.cshtml"
  
    ViewBag.Title = "Home Confirm Subscribe";
 

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-6 offset-3 text-center\">\r\n");
#nullable restore
#line 9 "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\Home\ConfirmSubscribe.cshtml"
         if (Model.Item2)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert alert-danger\">\r\n                ");
#nullable restore
#line 12 "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\Home\ConfirmSubscribe.cshtml"
           Write(Model.Item1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n");
#nullable restore
#line 14 "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\Home\ConfirmSubscribe.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert alert-success\">\r\n                ");
#nullable restore
#line 18 "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\Home\ConfirmSubscribe.cshtml"
           Write(Model.Item1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n");
#nullable restore
#line 20 "D:\code\Lessons\OganiProjectOnWindows\Ogani\Ogani.WebUI\Views\Home\ConfirmSubscribe.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Tuple<string,bool>> Html { get; private set; }
    }
}
#pragma warning restore 1591
