#pragma checksum "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1a2e9eee46beb478341748d794f5e599dc78c2c4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EditProfile_Index), @"mvc.1.0.view", @"/Views/EditProfile/Index.cshtml")]
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
#line 1 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\_ViewImports.cshtml"
using UMS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\_ViewImports.cshtml"
using UMS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1a2e9eee46beb478341748d794f5e599dc78c2c4", @"/Views/EditProfile/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"09e50ace8fe3b4184cf126cd47ae178edfbfa883", @"/Views/_ViewImports.cshtml")]
    public class Views_EditProfile_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EditProfile>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("acc_Id"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("acc_Firstname"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "acc_Firstname", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("acc_Lastname"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "acc_Lastname", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("FormProfile"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "editProfile", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "EditProfile", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
  
    ViewData["Title"] = "Edit Profile";
    Layout = "~/Views/Shared/AdminLTE/_Layout.cshtml";

    var editProfile = ViewData["User"] as EditProfile;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""mx-auto col-5"">
    <br>
    <!--Start Card-->
    <div class=""card card-info text-center"">
        <!--Start Card header-->
        <div class=""card-header"">
            <h3 class=""card-title"">Edit Profile</h3>
        </div>
        <!--End Card header-->
        <!--Start Card body-->
        <div class=""card-body"">
            <!--Start Form profile-->
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1a2e9eee46beb478341748d794f5e599dc78c2c47582", async() => {
                WriteLiteral("\r\n                <!--Start Input edit profile-->\r\n                <span class=\"text-danger small\"></span>\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "1a2e9eee46beb478341748d794f5e599dc78c2c47968", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 24 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.acc_Id);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 24 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
                                                             WriteLiteral(editProfile.acc_Id);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                <div class=\"input-group mb-3\">\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "1a2e9eee46beb478341748d794f5e599dc78c2c410519", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Name = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
#nullable restore
#line 26 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.acc_Firstname);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 26 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
                                                                                                           WriteLiteral(editProfile.acc_Firstname);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                </div>\r\n                <div class=\"input-group mb-3\">\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "1a2e9eee46beb478341748d794f5e599dc78c2c413235", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Name = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
#nullable restore
#line 29 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.acc_Lastname);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 29 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
                                                                                                        WriteLiteral(editProfile.acc_Lastname);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                </div>

                <!--Start Input change password-->
                <div id=""acc_OldPassword"" class=""input-group mb-3"">
                    <input id=""acc_Old"" type=""password"" name=""acc_OldPassword"" class=""form-control"" placeholder=""Old password"" />
                    <span id=""validation_acc_OldPassword"" class=""text-danger""></span>
                </div>
                <div id=""acc_NewPassword"" class=""input-group mb-3"">
                    <input id=""acc_New"" type=""password"" name=""acc_NewPassword"" class=""form-control"" placeholder=""New password""/>
                    <span id=""validation_acc_NewPassword"" class=""text-danger""></span>
                </div>
                <div id=""acc_ConfirmPassword"" class=""input-group mb-3"">
                    <input id=""acc_Con"" type=""password"" name=""acc_ConfirmPassword"" class=""form-control"" placeholder=""Confirm password"" />
                    <span id=""validation_acc_ConfirmPassword"" class=""text-danger""></span>
                </di");
                WriteLiteral(@"v>
                <!--End Input change password-->

                <!--Start Button save password-->
                <button id=""btn_SavePassword"" type=""submit"" class=""btn btn-success btn-block"">Save</button>
                <!--End Button save password-->

                <!--End Input edit profile-->

                <!--Start Button save profile-->
                <div class=""row"">
                    <div class=""col-8"">
                        <p id=""href_ChangePassword"">Change password</p>
                    </div>
                    <div class=""col-4"">
                        <button id=""btn_SaveProfile"" type=""submit"" class=""btn btn-success btn-block float-right"">Save</button>
                    </div>
                </div>
                <!--End Button save profile-->
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            <!--End Form profile-->
        </div>
        <!--End Card body-->
    </div>
    <!--Stop Card-->
</div>

<!--Start Script-->
<script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js""></script>
<script>
    $(document).ready(function () {
        $(""#acc_OldPassword"").hide();
        $(""#acc_NewPassword"").hide();
        $(""#acc_ConfirmPassword"").hide();
        $(""#btn_SavePassword"").hide();

        $(""#href_ChangePassword"").click(function () {
            $(""#href_ChangePassword"").hide();
            $(""#btn_SaveProfile"").hide();
            $(""#acc_OldPassword"").show();
            $(""#acc_NewPassword"").show();
            $(""#acc_ConfirmPassword"").show();
            $(""#btn_SavePassword"").show();
        });

        // Validation Form
        $(""#FormProfile"").keydown(function (e) {
            e.preventDefault();
            var acc_OldPassword = $('#acc_Old').val();
            var acc_NewPassword = $('#acc_New').val();
            va");
            WriteLiteral(@"r acc_ConfirmPassword = $('#acc_Con').val();

            //Regular expression
            var pattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$!%*?&])[A-Za-z0-9$!%*?&]+$/;

            // Validation if acc_NewPassword do not match with acc_ConfirmPassword.
            if (acc_NewPassword != acc_ConfirmPassword) {
                $('#validation_acc_ConfirmPassword').html('The password and confirmation password do not match.');
            } else {
                $('#validation_acc_ConfirmPassword').html('');
            }

            // Validation if acc_OldPassword do not math with Regular expression.
            if (!pattern.test(acc_OldPassword)) {
                $('#validation_acc_OldPassword').html('The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.');
            } else {
                $('#validation_acc_OldPassword').html('');
            }

            // Validation if acc_NewPassword do not math with Regular expression.
     ");
            WriteLiteral(@"       if (!pattern.test(acc_NewPassword)) {
                $('#validation_acc_NewPassword').html('The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.');
            } else {
                $('#validation_acc_NewPassword').html('');
            }
        });

        // Toastr if old password is not match with password hash in database.
");
#nullable restore
#line 122 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
         if (TempData["LoginErrorResult"] != null) { 

#line default
#line hidden
#nullable disable
#nullable restore
#line 122 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
                                                Write(Html.Raw(TempData["LoginErrorResult"]));

#line default
#line hidden
#nullable disable
#nullable restore
#line 122 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
                                                                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        // Toastr if EditProfile (Firstname and Lastname) Success.\r\n");
#nullable restore
#line 124 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
         if (TempData["LoginSuccessResult"] != null) { 

#line default
#line hidden
#nullable disable
#nullable restore
#line 124 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
                                                  Write(Html.Raw(TempData["LoginSuccessResult"]));

#line default
#line hidden
#nullable disable
#nullable restore
#line 124 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
                                                                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("        // Toastr if EditProfile (Firstname, Lastname and Change password) Success.\r\n");
#nullable restore
#line 126 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
         if (TempData["LoginAllSuccessResult"] != null) { 

#line default
#line hidden
#nullable disable
#nullable restore
#line 126 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
                                                     Write(Html.Raw(TempData["LoginAllSuccessResult"]));

#line default
#line hidden
#nullable disable
#nullable restore
#line 126 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\EditProfile\Index.cshtml"
                                                                                                      }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    })
</script>
<!--End Script-->

<!--Start Style-->
<style>
    #href_ChangePassword {
        text-align: left;
        color: blue;
        cursor: pointer;
    }
    #validation_acc_OldPassword {
        text-align: left;
    }
    #validation_acc_NewPassword {
        text-align: left;
    }
    #validation_acc_ConfirmPassword {
        text-align: left;
    }
</style>
<!--End Style-->");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EditProfile> Html { get; private set; }
    }
}
#pragma warning restore 1591
