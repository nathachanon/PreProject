#pragma checksum "D:\PreProject\Views\Manage\Main.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6c0645dfea71908a84356f22c55a29819b3245f9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Manage_Main), @"mvc.1.0.view", @"/Views/Manage/Main.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Manage/Main.cshtml", typeof(AspNetCore.Views_Manage_Main))]
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
#line 1 "D:\PreProject\Views\_ViewImports.cshtml"
using MASdemo;

#line default
#line hidden
#line 2 "D:\PreProject\Views\_ViewImports.cshtml"
using MASdemo.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c0645dfea71908a84356f22c55a29819b3245f9", @"/Views/Manage/Main.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0bcc08d842d430368c2639d651d68a817d624ed7", @"/Views/_ViewImports.cshtml")]
    public class Views_Manage_Main : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 3, true);
            WriteLiteral(" \r\n");
            EndContext();
#line 2 "D:\PreProject\Views\Manage\Main.cshtml"
   
    Layout = "_Layout"; 

#line default
#line hidden
            BeginContext(38, 43, true);
            WriteLiteral(" \r\n<!DOCTYPE html> \r\n<html lang=\"en\"> \r\n \r\n");
            EndContext();
            BeginContext(81, 196, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bd5c1d2059a44882837b6d9f627cff6b", async() => {
                BeginContext(87, 183, true);
                WriteLiteral(" \r\n  <!-- Required meta tags --> \r\n  <meta charset=\"utf-8\"> \r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1, shrink-to-fit=no\"> \r\n  <title>หน้าแรก</title> \r\n \r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(277, 6, true);
            WriteLiteral(" \r\n \r\n");
            EndContext();
            BeginContext(283, 8925, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "750f694369e64e9aa19ccc700a9934cf", async() => {
                BeginContext(289, 7, true);
                WriteLiteral(" \r\n    ");
                EndContext();
                BeginContext(297, 37, false);
#line 18 "D:\PreProject\Views\Manage\Main.cshtml"
Write(Html.Raw(TempData["loginSuccessful"]));

#line default
#line hidden
                EndContext();
                BeginContext(334, 8867, true);
                WriteLiteral(@" 
    <!-- partial:partials/_navbar.html --> 
    <!-- partial --> 
    <div class=""main-panel""> 
        <div class=""content-wrapper""> 
            <div class=""row""> 
                <div class=""col-xl-3 col-lg-3 col-md-3 col-sm-6 grid-margin stretch-card""> 
                    <div class=""card card-statistics""> 
                        <div class=""card-body""> 
                            <div class=""clearfix""> 
                                <div class=""float-left""> 
                                    <i class=""mdi mdi-cube text-danger icon-lg""></i> 
                                </div> 
                                <div class=""float-right""> 
                                    <p class=""mb-0 text-right"">รายได้ทั้งหมด</p> 
                                    <div class=""fluid-container""> 
                                        <h3 class=""font-weight-medium text-right mb-0"">฿65,650</h3> 
                                        <p class=""mb-0 text-right"">บาท</p> 
                    ");
                WriteLiteral(@"                </div> 
                                </div> 
                            </div> 
                            <p class=""text-muted mt-3 mb-0""> 
                                <i class=""mdi mdi-alert-octagon mr-1"" aria-hidden=""true""></i> เพิ่มขึ้น 65% 
                            </p> 
                        </div> 
                    </div> 
                </div> 
                <div class=""col-xl-3 col-lg-3 col-md-3 col-sm-6 grid-margin stretch-card""> 
                    <div class=""card card-statistics""> 
                        <div class=""card-body""> 
                            <div class=""clearfix""> 
                                <div class=""float-left""> 
                                    <i class=""mdi mdi-account-card-details text-success icon-lg""></i> 
                                </div> 
                                <div class=""float-right""> 
                                    <p class=""mb-0 text-right"">จำนวน ผู้เช่า</p> 
                          ");
                WriteLiteral(@"          <div class=""fluid-container""> 
                                        <h3 class=""font-weight-medium text-right mb-0"">45</h3> 
                                        <p class=""mb-0 text-right"">คน</p> 
                                    </div> 
                                </div> 
                            </div> 
                            <p class=""text-muted mt-3 mb-0""> 
                                <i class=""mdi mdi-bookmark-outline mr-1"" aria-hidden=""true""></i> ข้อมูลล่าสุด 
                            </p> 
                        </div> 
                    </div> 
                </div> 
                <div class=""col-xl-3 col-lg-3 col-md-3 col-sm-6 grid-margin stretch-card""> 
                    <div class=""card card-statistics""> 
                        <div class=""card-body""> 
                            <div class=""clearfix""> 
                                <div class=""float-left""> 
                                    <i class=""mdi mdi-poll-box text-success i");
                WriteLiteral(@"con-lg""></i> 
                                </div> 
                                <div class=""float-right""> 
                                    <p class=""mb-0 text-right"">จำนวน ห้องว่าง</p> 
                                    <div class=""fluid-container""> 
                                        <h3 class=""font-weight-medium text-right mb-0"">23</h3> 
                                        <p class=""mb-0 text-right"">ห้อง</p> 
                                    </div> 
                                </div> 
                            </div> 
                            <p class=""text-muted mt-3 mb-0""> 
                                <i class=""mdi mdi-calendar mr-1"" aria-hidden=""true""></i> ข้อมูลล่าสุด 
                            </p> 
                        </div> 
                    </div> 
                </div> 
                <div class=""col-xl-3 col-lg-3 col-md-3 col-sm-6 grid-margin stretch-card""> 
                    <div class=""card card-statistics""> 
                   ");
                WriteLiteral(@"     <div class=""card-body""> 
                            <div class=""clearfix""> 
                                <div class=""float-left""> 
                                    <i class=""mdi mdi-account-location text-info icon-lg""></i> 
                                </div> 
                                <div class=""float-right""> 
                                    <p class=""mb-0 text-right"">พนักงาน</p> 
                                    <div class=""fluid-container""> 
                                        <h3 class=""font-weight-medium text-right mb-0"">0</h3> 
                                        <p class=""mb-0 text-right"">คน</p> 
                                    </div> 
                                </div> 
                            </div> 
                            <p class=""text-muted mt-3 mb-0""> 
                                <i class=""mdi mdi-reload mr-1"" aria-hidden=""true""></i> ข้อมูลล่าสุด 
                            </p> 
                        </div> 
          ");
                WriteLiteral(@"          </div> 
                </div> 
            </div> 
            <div class=""row""> 
                <div class=""col-lg-8 grid-margin stretch-card""> 
                    <!--กราฟ--> 
                    <div class=""card""> 
                        <div class=""card-body text-center""> 
                            <div class=""row d-none d-sm-flex mb-4 text-center""> 
 
                                <h4 class=""text-primary "">กราฟ test Layout</h4> 
 
 
 
                            </div> 
                            <div class=""chart-container""> 
                                <canvas id=""dashboard-area-chart"" height=""80""></canvas> 
                            </div> 
                        </div> 
                    </div> 
                </div> 
                <!--กราฟ จบ--> 
 
                <div class=""col-lg-4 grid-margin stretch-card""> 
                    <div class=""card""> 
                        <div class=""card-body""> 
                            <h2 class=""card-");
                WriteLiteral(@"title text-primary mb-5"">test test</h2> 
                            <div class=""wrapper d-flex justify-content-between""> 
                                <div class=""side-left""> 
                                    <p class=""mb-2"">test test</p> 
                                    <p class=""display-3 mb-4 font-weight-light"">+45.2%</p> 
                                </div> 
                                <div class=""side-right""> 
                                    <small class=""text-muted"">2017</small> 
                                </div> 
                            </div> 
                            <div class=""wrapper d-flex justify-content-between""> 
                                <div class=""side-left""> 
                                    <p class=""mb-2"">test test</p> 
                                    <p class=""display-3 mb-5 font-weight-light"">-35.3%</p> 
                                </div> 
                                <div class=""side-right""> 
                       ");
                WriteLiteral(@"             <small class=""text-muted"">2015</small> 
                                </div> 
                            </div> 
                            <div class=""wrapper""> 
                                <div class=""d-flex justify-content-between""> 
                                    <p class=""mb-2"">รายได้</p> 
                                    <p class=""mb-2 text-primary"">88%</p> 
                                </div> 
                                <div class=""progress""> 
                                    <div class=""progress-bar bg-primary progress-bar-striped progress-bar-animated"" role=""progressbar"" style=""width: 88%"" aria-valuenow=""88"" 
                                         aria-valuemin=""0"" aria-valuemax=""100""></div> 
                                </div> 
                            </div> 
                            <div class=""wrapper mt-4""> 
                                <div class=""d-flex justify-content-between""> 
                                    <p class=""");
                WriteLiteral(@"mb-2"">รายได้</p> 
                                    <p class=""mb-2 text-success"">56%</p> 
                                </div> 
                                <div class=""progress""> 
                                    <div class=""progress-bar bg-success progress-bar-striped progress-bar-animated"" role=""progressbar"" style=""width: 56%"" aria-valuenow=""56"" 
                                         aria-valuemin=""0"" aria-valuemax=""100""></div> 
                                </div> 
                            </div> 
                        </div> 
                    </div> 
                </div> 
            </div> 
        </div> 

    </div> 

");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(9208, 16, true);
            WriteLiteral(" \r\n \r\n</html> \r\n");
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
