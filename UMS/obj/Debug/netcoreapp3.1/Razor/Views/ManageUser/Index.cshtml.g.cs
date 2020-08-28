#pragma checksum "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "776a7e34a724921edd5bde91209b763dd27b0952"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ManageUser_Index), @"mvc.1.0.view", @"/Views/ManageUser/Index.cshtml")]
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
#line 1 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\_ViewImports.cshtml"
using UMS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\_ViewImports.cshtml"
using UMS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"776a7e34a724921edd5bde91209b763dd27b0952", @"/Views/ManageUser/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"09e50ace8fe3b4184cf126cd47ae178edfbfa883", @"/Views/_ViewImports.cshtml")]
    public class Views_ManageUser_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Account>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("ModalEditUser"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
  
    ViewData["Title"] = "Manage User";
    Layout = "~/Views/Shared/AdminLTE/_Layout.cshtml";

    var acc = @ViewData["User"] as Account;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<!--
    Namspace: ~/View/ManageUser/Index.cshtml
    Author: Namchok Singhachai
    Description: view for user management
-->

<div class=""mr-auto"">

    <!--Start Card-->
    <div class=""card card-info mt-2 w-auto mx-auto"">
        <!--Start Card header-->
        <div class=""card-header"">
            <h3 class=""card-title"">Manage Users</h3>
        </div>
        <!--End Card header-->
        <!--Start Card body-->
        <div class=""card-body"">
            <div class=""box"">
                <div class=""box-body"">
                    <!--Start Manage user table-->
                    <div class=""table-responsive-md"">
                        <table id=""showAllUserTable"" class=""table table-hover 
                                w-100 table-bordered table-striped"">
                        <!--Start Manage user table head-->
                        <thead class=""text-center"">
                            <tr>
                                <th>No.</th>
                            ");
            WriteLiteral(@"    <th>Username</th>
                                <th>Name</th>
                                <th>Role</th>
                                <th>Type Account</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <!--End Manage user table head-->
                        <!--Start Manage user table body-->
                        <tbody>
");
#nullable restore
#line 47 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
                               int index = 0; 

#line default
#line hidden
#nullable disable
#nullable restore
#line 48 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
                             if (ViewData["User"] != null)
                            {
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 50 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
                                 foreach (var item in ViewData["User"] as List<Account>)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td><center>");
#nullable restore
#line 53 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
                                                Write(index += 1);

#line default
#line hidden
#nullable disable
            WriteLiteral("</center></td>\r\n                                        <td>");
#nullable restore
#line 54 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
                                       Write(item.acc_Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 55 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
                                        Write(item.acc_Firstname + " " + item.acc_Lastname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td><center> ");
#nullable restore
#line 56 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
                                                 Write((item.acc_Rolename != null) ? item.acc_Rolename : "-");

#line default
#line hidden
#nullable disable
            WriteLiteral(" </center></td>\r\n                                        <td><center> ");
#nullable restore
#line 57 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
                                                 Write((item.acc_TypeAccoutname != null) ? item.acc_TypeAccoutname : "-");

#line default
#line hidden
#nullable disable
            WriteLiteral(" </center></td>\r\n                                        <td class=\"text-center\">\r\n                                            <button class=\"btn btn-warning text-white\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2604, "\"", 2637, 3);
            WriteAttributeValue("", 2614, "getUser(\'", 2614, 9, true);
#nullable restore
#line 59 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
WriteAttributeValue("", 2623, item.acc_Id, 2623, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2635, "\')", 2635, 2, true);
            EndWriteAttribute();
            WriteLiteral("><i class=\"fa fa-pencil-alt\"></i></button>\r\n                                            <button class=\"btn btn-danger\"><i class=\"fa fa-times\"></i></button>\r\n                                        </td>\r\n                                    </tr>\r\n");
#nullable restore
#line 63 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 63 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
                                 
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td colspan=\"6\"><center>No Users.</center></td>\r\n                                </tr>\r\n");
#nullable restore
#line 70 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </tbody>
                        <!--End Manage user table body-->
                    </table>
                    </div>
                    <!--End Manage user table body-->
                </div>
                <!--End Manage user table body-->
            </div>
            <!--End Manage user table-->
        </div>
        <!--End Card body-->

        <!--Start Modal edit user-->
        <div class=""modal fade"" id=""EditUser"">
            <div class=""modal-dialog"">
                <div class=""modal-content"">
                    <!--Start Modal header edit user-->
                    <div class=""modal-header bg-warning"">
                        <h4 class=""modal-title  text-white"">EDIT ACCOUNT.</h4>
                        <!--Start Button close-->
                        <button type=""button"" class=""close text-white"" data-dismiss=""modal"" aria-label=""Close"">
                            <span aria-hidden=""true"">&times;</span>
                        </button>");
            WriteLiteral(@"
                        <!--End Button close-->
                    </div>
                    <!--End Modal header edit user-->
                    <!--Start Modal body edit user-->
                    <div class=""modal-body"">
                        <!--Start Form Edit user-->
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "776a7e34a724921edd5bde91209b763dd27b095212509", async() => {
                WriteLiteral("\r\n                            <!--Start Input Edit user-->\r\n                            <div class=\"form-group\">\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "776a7e34a724921edd5bde91209b763dd27b095212918", async() => {
                    WriteLiteral("First Name");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 103 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.acc_Firstname);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                <input type=\"text\" class=\"form-control\" id=\"acc_Firstname\" placeholder=\"First Name\"");
                BeginWriteAttribute("value", " value=\"", 4896, "\"", 4904, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                            </div>\r\n\r\n                            <div class=\"form-group\">\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "776a7e34a724921edd5bde91209b763dd27b095214870", async() => {
                    WriteLiteral("Last Name");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 108 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.acc_Lastname);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                <input type=\"text\" class=\"form-control\" id=\"acc_Lastname\" placeholder=\"Last Name\"");
                BeginWriteAttribute("value", " value=\"", 5194, "\"", 5202, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                            </div>\r\n\r\n                            <div class=\"form-group\">\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "776a7e34a724921edd5bde91209b763dd27b095216818", async() => {
                    WriteLiteral("Role");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 113 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.acc_Rolename);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                <select class=\"form-control\" id=\"acc_Rolename\">\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "776a7e34a724921edd5bde91209b763dd27b095218472", async() => {
                    WriteLiteral("Choose");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("disabled", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "776a7e34a724921edd5bde91209b763dd27b095220378", async() => {
                    WriteLiteral("Admin");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "776a7e34a724921edd5bde91209b763dd27b095221637", async() => {
                    WriteLiteral("User");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                </select>\r\n                            </div>\r\n                            <!--End Input Edit user-->\r\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                        <!--End Form Edit user-->
                    </div>
                    <!--End Modal body-->
                    <!--Start Modal footer-->
                    <div class=""modal-footer justify-content-between"">
                        <button type=""button"" class=""btn btn-default float-left"" data-dismiss=""modal"">Back</button>
                        <button type=""button"" class=""btn btn-success float-right"">Save</button>
                    </div>
                    <!--End Modal footer-->
                </div>
            </div>
        </div>
        <!--End Modal Edit user-->

    </div>
    <!--End Card-->
</div>

<script>

    function getUser(id) {
        $.ajax({
            url: """);
#nullable restore
#line 144 "C:\Users\Namchok\Desktop\สหกิจ\ums\UMS\Views\ManageUser\Index.cshtml"
             Write(Url.Action("getUser", "ManageUser"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
            data: { id: id },
            type: ""POST"",
            dataType: ""JSON"",
            success: function (result) {
                console.log(result[0]);

                $('#acc_Firstname').val(result[0].acc_Firstname);
                $('#acc_Lastname').val(result[0].acc_Lastname);

                if (result[0].acc_Rolename === 'Admin') {
                    $('#acc_Rolename').val(1);
                } else if (result[0].acc_Rolename === 'User') {
                    $('#acc_Rolename').val(2);
                } else {
                    $('#acc_Rolename').val(0);
                }

                $('#EditUser').modal();
                console.log('success~');

            },
            error: function (result) {
                console.log(result.responseText)
            }
        })
    }
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Account> Html { get; private set; }
    }
}
#pragma warning restore 1591
