#pragma checksum "E:\PreProject\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7b85beec0a6d02d1545b69a4b7f6d843f831b5e2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/About.cshtml", typeof(AspNetCore.Views_Home_About))]
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
#line 1 "E:\PreProject\Views\_ViewImports.cshtml"
using MASdemo;

#line default
#line hidden
#line 2 "E:\PreProject\Views\_ViewImports.cshtml"
using MASdemo.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7b85beec0a6d02d1545b69a4b7f6d843f831b5e2", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0bcc08d842d430368c2639d651d68a817d624ed7", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "E:\PreProject\Views\Home\About.cshtml"
  
    ViewData["Title"] = "About";

#line default
#line hidden
#line 4 "E:\PreProject\Views\Home\About.cshtml"
  
    if (ViewBag.myLog == "0")
    {

#line default
#line hidden
            BeginContext(83, 46, true);
            WriteLiteral("        <h1>Status Login : Unsuccessful</h1>\r\n");
            EndContext();
#line 8 "E:\PreProject\Views\Home\About.cshtml"
    }
    else if (ViewBag.myLog == "1")
    {

#line default
#line hidden
            BeginContext(179, 41, true);
            WriteLiteral("        <h1>Status Login : Success</h1>\r\n");
            EndContext();
            BeginContext(222, 21, true);
            WriteLiteral("        <p>My Name : ");
            EndContext();
            BeginContext(244, 14, false);
#line 13 "E:\PreProject\Views\Home\About.cshtml"
                Write(ViewBag.myName);

#line default
#line hidden
            EndContext();
            BeginContext(258, 30, true);
            WriteLiteral("</p>\r\n        <p>My Surname : ");
            EndContext();
            BeginContext(289, 17, false);
#line 14 "E:\PreProject\Views\Home\About.cshtml"
                   Write(ViewBag.mySurname);

#line default
#line hidden
            EndContext();
            BeginContext(306, 28, true);
            WriteLiteral("</p>\r\n        <p>My Email : ");
            EndContext();
            BeginContext(335, 15, false);
#line 15 "E:\PreProject\Views\Home\About.cshtml"
                 Write(ViewBag.myEmail);

#line default
#line hidden
            EndContext();
            BeginContext(350, 26, true);
            WriteLiteral("</p>\r\n        <p>My Tel : ");
            EndContext();
            BeginContext(377, 13, false);
#line 16 "E:\PreProject\Views\Home\About.cshtml"
               Write(ViewBag.myTel);

#line default
#line hidden
            EndContext();
            BeginContext(390, 6, true);
            WriteLiteral("</p>\r\n");
            EndContext();
#line 17 "E:\PreProject\Views\Home\About.cshtml"
    }

#line default
#line hidden
            BeginContext(406, 2, true);
            WriteLiteral("\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
