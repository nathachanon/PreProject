#pragma checksum "E:\PreProject\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a10c3a7f997484a5d978619c9ea7f45f76a1772a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a10c3a7f997484a5d978619c9ea7f45f76a1772a", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0bcc08d842d430368c2639d651d68a817d624ed7", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "E:\PreProject\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#line 4 "E:\PreProject\Views\Home\Index.cshtml"
  
    if (ViewBag.Log == "1")
    {

#line default
#line hidden
            BeginContext(85, 45, true);
            WriteLiteral("        <h1>Status Login Log : Success</h1>\r\n");
            EndContext();
#line 8 "E:\PreProject\Views\Home\Index.cshtml"
    }
    if (ViewBag.myLog == "0")
    {

#line default
#line hidden
            BeginContext(175, 46, true);
            WriteLiteral("        <h1>Status Login : Unsuccessful</h1>\r\n");
            EndContext();
#line 12 "E:\PreProject\Views\Home\Index.cshtml"
    }
    else if (ViewBag.myLog == "1")
    {

#line default
#line hidden
            BeginContext(271, 41, true);
            WriteLiteral("        <h1>Status Login : Success</h1>\r\n");
            EndContext();
            BeginContext(314, 21, true);
            WriteLiteral("        <p>My Name : ");
            EndContext();
            BeginContext(336, 14, false);
#line 17 "E:\PreProject\Views\Home\Index.cshtml"
                Write(ViewBag.myName);

#line default
#line hidden
            EndContext();
            BeginContext(350, 30, true);
            WriteLiteral("</p>\r\n        <p>My Surname : ");
            EndContext();
            BeginContext(381, 17, false);
#line 18 "E:\PreProject\Views\Home\Index.cshtml"
                   Write(ViewBag.mySurname);

#line default
#line hidden
            EndContext();
            BeginContext(398, 28, true);
            WriteLiteral("</p>\r\n        <p>My Email : ");
            EndContext();
            BeginContext(427, 15, false);
#line 19 "E:\PreProject\Views\Home\Index.cshtml"
                 Write(ViewBag.myEmail);

#line default
#line hidden
            EndContext();
            BeginContext(442, 26, true);
            WriteLiteral("</p>\r\n        <p>My Tel : ");
            EndContext();
            BeginContext(469, 13, false);
#line 20 "E:\PreProject\Views\Home\Index.cshtml"
               Write(ViewBag.myTel);

#line default
#line hidden
            EndContext();
            BeginContext(482, 6, true);
            WriteLiteral("</p>\r\n");
            EndContext();
#line 21 "E:\PreProject\Views\Home\Index.cshtml"
    }

#line default
#line hidden
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
