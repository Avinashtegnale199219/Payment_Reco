#pragma checksum "E:\Working Drive\100004659\repo-mf-fd-payment-reconciliation\FD_PaymentReconciliation_V2\Views\BankRevalidationRequestDashboard_L2\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1995783306c3d989be4a39d968b2982b6722cc9df5c228a385e6ed67ab7c1ebb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BankRevalidationRequestDashboard_L2_Index), @"mvc.1.0.view", @"/Views/BankRevalidationRequestDashboard_L2/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BankRevalidationRequestDashboard_L2/Index.cshtml", typeof(AspNetCore.Views_BankRevalidationRequestDashboard_L2_Index))]
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
#line 1 "E:\Working Drive\100004659\repo-mf-fd-payment-reconciliation\FD_PaymentReconciliation_V2\Views\_ViewImports.cshtml"
using FD_PaymentReconciliation_V2;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1995783306c3d989be4a39d968b2982b6722cc9df5c228a385e6ed67ab7c1ebb", @"/Views/BankRevalidationRequestDashboard_L2/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"9ead039fc00fc0fb71d241bd986afcdc3cc455fa6df362f0dbbee5c3892e442b", @"/Views/_ViewImports.cshtml")]
    public class Views_BankRevalidationRequestDashboard_L2_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/AppJs/BankRevalidationRequestDashboardL2.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/jquery.dataTables.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "E:\Working Drive\100004659\repo-mf-fd-payment-reconciliation\FD_PaymentReconciliation_V2\Views\BankRevalidationRequestDashboard_L2\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(43, 111, true);
            WriteLiteral("\r\n<script type=\"text/javascript\">\r\n    function InitFunction() {\r\n        var objectURLPath = { uploadedData: \'");
            EndContext();
            BeginContext(155, 58, false);
#line 8 "E:\Working Drive\100004659\repo-mf-fd-payment-reconciliation\FD_PaymentReconciliation_V2\Views\BankRevalidationRequestDashboard_L2\Index.cshtml"
                                        Write(Url.Action("Index", "BankRevalidationRequestDashboard_L2"));

#line default
#line hidden
            EndContext();
            BeginContext(213, 1878, true);
            WriteLiteral(@"'};
            return objectURLPath;
        }
</script>
<div class=""container"">
    <div id=""preloader"" style=""display: none;"">
        <div class=""overlay"">
            <p>
                Loading
                <span class=""spinner""></span>
            </p>
        </div>
    </div>
    <br /><br />
    <div id=""dvMain"" class=""panel panel-default"">

        <div class=""panel-body"">
            <div class=""form-group"">

                <div class=""row"" style=""padding-bottom: 10px;"">
                    <div class=""col-md-12"">
                        <div class=""coman_table_head  table-responsive"">
                            <table id=""tblBankRevalidationRequestDashboard"" class=""table table-default"">
                                <thead>
                                    <tr>
                                        <th style=""display:none"">Request No</th>
                                        <th>Select</th>
                                        <th>IW No</th>
         ");
            WriteLiteral(@"                               <th>FDR No</th>
                                        <th>Folio No</th>
                                        <th>Depositor Name</th>
                                        <th>Amount</th>
                                        <th>PAN</th>
                                        <th>OFAS voucher No</th>
                                        <th>Payment Status</th>
                                        <th>Approved By</th>
                                        <th>Approved Date</th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</div>
");
            EndContext();
            BeginContext(2091, 69, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1995783306c3d989be4a39d968b2982b6722cc9df5c228a385e6ed67ab7c1ebb7104", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2160, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(2162, 53, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1995783306c3d989be4a39d968b2982b6722cc9df5c228a385e6ed67ab7c1ebb8307", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2215, 2, true);
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
