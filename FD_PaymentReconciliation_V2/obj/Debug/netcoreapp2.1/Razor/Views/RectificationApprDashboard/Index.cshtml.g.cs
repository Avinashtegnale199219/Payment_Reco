#pragma checksum "E:\Working Drive\100004659\repo-mf-fd-payment-reconciliation\FD_PaymentReconciliation_V2\Views\RectificationApprDashboard\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9d23da469f05709271f875b2a64d729023e7c4157c19b170f0c572271f6644a0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_RectificationApprDashboard_Index), @"mvc.1.0.view", @"/Views/RectificationApprDashboard/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/RectificationApprDashboard/Index.cshtml", typeof(AspNetCore.Views_RectificationApprDashboard_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"9d23da469f05709271f875b2a64d729023e7c4157c19b170f0c572271f6644a0", @"/Views/RectificationApprDashboard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"9ead039fc00fc0fb71d241bd986afcdc3cc455fa6df362f0dbbee5c3892e442b", @"/Views/_ViewImports.cshtml")]
    public class Views_RectificationApprDashboard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/jquery.dataTables.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/jquery.dataTables.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/AppJs/RectificationApprDashboard.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "E:\Working Drive\100004659\repo-mf-fd-payment-reconciliation\FD_PaymentReconciliation_V2\Views\RectificationApprDashboard\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(43, 64, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "9d23da469f05709271f875b2a64d729023e7c4157c19b170f0c572271f6644a05193", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(107, 4612, true);
            WriteLiteral(@"
<div class=""panel panel-default"" id=""divSearch"">

    <div class=""panel-body"">
        <div style=""height: 300px; overflow: auto;"">
            <div class=""table-responsive"">
                <table class=""table table-default"" id=""tbl_RectificationList"">
                    <thead>
                        <tr>
                            <th>Sequence No.</th>
                            <th>Rectification Sequence No.</th>
                            <th>Created By</th>
                            <th>Created Date</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody id=""tbody_RectificationList"">
                        <tr>
                            <td colspan=""4"" style=""text-align: left;"">No Details Found</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
<div class=""panel panel-default"" id=""divApproval"" style=""displa");
            WriteLiteral(@"y:none"">
    <div class=""panel-heading"">
        Rectification Approval List
        <a href=""javascript:Reset();"" class=""btn btn-red btn-medium pull-right"" style=""padding: 2px 8px; margin-top: -2px;"">Back</a>
    </div>
    <div class=""panel-body"">
        <div style=""max-height: 300px; overflow: auto;"">
            <div class=""table-responsive"">
                <table class=""table table-default"" id=""tbl_RectList"">
                    <thead>
                        <tr>
                            <td colspan=""3""></td>
                            <td colspan=""4"" style=""text-align: center;""><b>FD Details</b></td>
                            <td colspan=""4"" style=""text-align: center;""><b>Payment Details</b></td>
                        </tr>
                        <tr>
                            <th>
                                <input type=""checkbox"" id=""chkSelectAllCalls"" class=""SearchAll"" />
                            </th>
                            <th>App No.</th>
            ");
            WriteLiteral(@"                <th>Cust Name.</th>
                            <th>FD Amt</th>
                            <th>Trans ID</th>
                            <th>Trans Date</th>
                            <th>Status</th>
                            <th>Trans Amt</th>
                            <th>Trans ID</th>
                            <th>Trans Date</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody id=""tbody_RectList"">
                        <tr>
                            <td colspan=""8"" style=""text-align: left;"">No Details Found</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div id=""div_Remarks"">
                <br />
                <div class=""pull-left""><b>Requestor Remarks: <span class=""red-text ""> *</span></b></div>
                <div class=""pull-left"" id=""Remarks""></div>
                <input type=""hidden"" id=""hdnHd");
            WriteLiteral(@"rReq"" />
                <input type=""hidden"" id=""hdnHdrRectReq"" />



            </div>
        </div>
        <div class=""row"">
            <div class=""panel-body hidden"" id=""div_Rectify"">
                <div class=""row"">
                    <div class=""col-sm-3"" style=""word-wrap: break-word;"">
                        <span class=""pull-right text-primary"" style=""padding-right: 2px; font-size: 11px; text-align: right;"">Max: <span id=""lblCountCharTextarea_1"" class=""lblCounter"">500</span></span>
                        <div>
                            <textarea class=""form-control"" id=""txt_remarks"" cols=""5"" rows=""3"" placeholder=""Remarks"" tabindex=""1"" style=""resize: none;"" maxlength=""500"" onkeyup=""Javascript:CharactersCount(this.id,lblCountCharTextarea_1);""></textarea>
                        </div>
                    </div>
                </div>
                <br />
                <div class=""row"">
                    <div class=""col-sm-1"">
                        <input type=""butto");
            WriteLiteral(@"n"" id=""btn_Approve"" value=""Approve"" class=""btn btn-red"" />
                    </div>
                    <div class=""col-sm-1"">
                        <input type=""button"" id=""btn_Reject"" value=""Reject"" class=""btn btn-red"" />
                    </div>
                    <div class=""col-sm-1 row"">
                        <input type=""button"" id=""btn_cancel"" value=""Cancel"" class=""btn btn-red"" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
");
            EndContext();
            BeginContext(4719, 53, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9d23da469f05709271f875b2a64d729023e7c4157c19b170f0c572271f6644a011364", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4772, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(4774, 87, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9d23da469f05709271f875b2a64d729023e7c4157c19b170f0c572271f6644a012568", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#line 107 "E:\Working Drive\100004659\repo-mf-fd-payment-reconciliation\FD_PaymentReconciliation_V2\Views\RectificationApprDashboard\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4861, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(5263, 2, true);
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
