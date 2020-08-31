#pragma checksum "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\ManageUser\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a37c893e4a89f0ec684b2ab21a2249a8238d88ed"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a37c893e4a89f0ec684b2ab21a2249a8238d88ed", @"/Views/ManageUser/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"09e50ace8fe3b4184cf126cd47ae178edfbfa883", @"/Views/_ViewImports.cshtml")]
    public class Views_ManageUser_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Account>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("ModalEditUser"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\ManageUser\Index.cshtml"
  
    ViewData["Title"] = "Manage User";
    Layout = "~/Views/Shared/AdminLTE/_Layout.cshtml";

    var acc = @ViewData["User"] as Account;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 10 "C:\Users\Wannapa\Source\Repos\ums\UMS\Views\ManageUser\Index.cshtml"
Write(acc.acc_Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>

<div class=""mr-auto"">
    <br>
    <!--Start Card-->
    <div class=""card card-info"">
        <!--Start Card header-->
        <div class=""card-header"">
            <h3 class=""card-title"">Manage users</h3>
        </div>
        <!--End Card header-->
        <!--Start Card body-->
        <div class=""card-body"">
            <div class=""box"">
                <div class=""box-body"">
                    <!--Start Manage user table-->
                    <table id=""example1"" class=""table table-bordered table-striped"">
                        <!--Start Manage user table head-->
                        <thead class=""text-center"">
                            <tr>
                                <th>No.</th>
                                <th>Firstname</th>
                                <th>Lastname</th>
                                <th>Email</th>
                                <th>Role</th>
                                <th>Actions</th>
                            </tr>
   ");
            WriteLiteral(@"                     </thead>
                        <!--End Manage user table head-->
                        <!--Start Manage user table body-->
                        <tbody>
                            <tr>
                                <td><center>1</center></td>
                                <td>Namchock</td>
                                <td>Singhachai</td>
                                <td>namchock@gmail.com</td>
                                <td><center>Admin</center></td>
                                <td class=""text-center"">
                                    <button class=""btn btn-warning text-white"" data-toggle=""modal"" data-target=""#EditUser""><i class=""fa fa-pencil-alt""></i></button>
                                    <button class=""btn btn-danger""><i class=""fa fa-times""></i></button>
                                </td>
                            </tr>
                            <tr>
                                <td><center>2</center></td>
                 ");
            WriteLiteral(@"               <td>Wannapa</td>
                                <td>Srijermtong</td>
                                <td>wannapa@hotmail.com</td>
                                <td><center>User</center></td>
                                <td class=""text-center"">
                                    <button class=""btn btn-warning text-white"" data-toggle=""modal"" data-target=""#EditUser""><i class=""fa fa-pencil-alt""></i></button>
                                    <button class=""btn btn-danger""><i class=""fa fa-times""></i></button>
                                </td>
                            </tr>
                            <tr>
                                <td><center>3</center></td>
                                <td>Arthiruj</td>
                                <td>Phusitaporn</td>
                                <td>arthiruj101@hotmail.com</td>
                                <td><center>User</center></td>
                                <td class=""text-center"">
                    ");
            WriteLiteral(@"                <button class=""btn btn-warning text-white"" data-toggle=""modal"" data-target=""#EditUser""><i class=""fa fa-pencil-alt""></i></button>
                                    <button class=""btn btn-danger""><i class=""fa fa-times""></i></button>
                                </td>
                            </tr>
                            <tr>
                                <td><center>4</center></td>
                                <td>Yaowapa</td>
                                <td>Pongpadcha</td>
                                <td>yaowapaa@msn.com</td>
                                <td><center>User</center></td>
                                <td class=""text-center"">
                                    <button class=""btn btn-warning text-white"" data-toggle=""modal"" data-target=""#EditUser""><i class=""fa fa-pencil-alt""></i></button>
                                    <button class=""btn btn-danger""><i class=""fa fa-times""></i></button>
                                </td>
          ");
            WriteLiteral(@"                  </tr>
                            <tr>
                                <td><center>5</center></td>
                                <td>Theo</td>
                                <td>Saetun</td>
                                <td>60160109@go.buu.ac.th</td>
                                <td><center>User</center></td>
                                <td class=""text-center"">
                                    <button class=""btn btn-warning text-white"" data-toggle=""modal"" data-target=""#EditUser""><i class=""fa fa-pencil-alt""></i></button>
                                    <button class=""btn btn-danger""><i class=""fa fa-times""></i></button>
                                </td>
                            </tr>
                            <tr>
                                <td><center>6</center></td>
                                <td>Phonlayut</td>
                                <td>Sophak</td>
                                <td>60160093@go.buu.ac.th</td>
                 ");
            WriteLiteral(@"               <td><center>User</center></td>
                                <td class=""text-center"">
                                    <button class=""btn btn-warning text-white"" data-toggle=""modal"" data-target=""#EditUser""><i class=""fa fa-pencil-alt""></i></button>
                                    <button class=""btn btn-danger""><i class=""fa fa-times""></i></button>
                                </td>
                            </tr>
                            <tr>
                                <td><center>7</center></td>
                                <td>Chanakan</td>
                                <td>Hokee</td>
                                <td>60160334@go.buu.ac.th</td>
                                <td><center>User</center></td>
                                <td class=""text-center"">
                                    <button class=""btn btn-warning text-white"" data-toggle=""modal"" data-target=""#EditUser""><i class=""fa fa-pencil-alt""></i></button>
                            ");
            WriteLiteral(@"        <button class=""btn btn-danger""><i class=""fa fa-times""></i></button>
                                </td>
                            </tr>
                            <tr>
                                <td><center>8</center></td>
                                <td>Manita</td>
                                <td>Doungrassamee</td>
                                <td>60160023@go.buu.ac.th</td>
                                <td><center>User</center></td>
                                <td class=""text-center"">
                                    <button class=""btn btn-warning text-white"" data-toggle=""modal"" data-target=""#EditUser""><i class=""fa fa-pencil-alt""></i></button>
                                    <button class=""btn btn-danger""><i class=""fa fa-times""></i></button>
                                </td>
                            </tr>
                            <tr>
                                <td><center>9</center></td>
                                <td>Vartinee</t");
            WriteLiteral(@"d>
                                <td>Teangthong</td>
                                <td>60160345@go.buu.ac.th</td>
                                <td><center>User</center></td>
                                <td class=""text-center"">
                                    <button class=""btn btn-warning text-white"" data-toggle=""modal"" data-target=""#EditUser""><i class=""fa fa-pencil-alt""></i></button>
                                    <button class=""btn btn-danger""><i class=""fa fa-times""></i></button>
                                </td>
                            </tr>
                        </tbody>
                        <!--End Manage user table body-->
                    </table>
                    <!--End Manage user table body-->
                </div>
                <!--End Manage user table body-->
            </div>
            <!--End Manage user table-->
        </div>
        <!--End Card body-->
        <!--Start Modal edit user-->
        <div class=""modal fade"" id=""E");
            WriteLiteral(@"ditUser"">
            <div class=""modal-dialog"">
                <div class=""modal-content"">
                    <!--Start Modal header edit user-->
                    <div class=""modal-header bg-warning"">
                        <h4 class=""modal-title  text-white"">Edit user account.</h4>
                        <!--Start Button close-->
                        <button type=""button"" class=""close text-white"" data-dismiss=""modal"" aria-label=""Close"">
                            <span aria-hidden=""true"">&times;</span>
                        </button>
                        <!--End Button close-->
                    </div>
                    <!--End Modal header edit user-->
                    <!--Start Modal body edit user-->
                    <div class=""modal-body"">
                        <!--Start Form Edit user-->
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a37c893e4a89f0ec684b2ab21a2249a8238d88ed13991", async() => {
                WriteLiteral(@"
                            <!--Start Input Edit user-->
                            <div class=""row"">
                                <label class=""col-sm-3 col-form-label"">Firstname:</label>
                                <div class=""col-sm-9 mr-auto"">
                                    <div class=""form-group"">
                                        <input class=""form-control"">
                                        <span class=""text-danger""></span>
                                    </div>
                                </div>
                                <label class=""col-sm-3 col-form-label"">Lastname:</label>
                                <div class=""col-sm-9 mr-auto"">
                                    <div class=""form-group"">
                                        <input class=""form-control"">
                                        <input class=""form-control"">
                                        <span class=""text-danger""></span>
                                    </di");
                WriteLiteral(@"v>
                                </div>
                                <label class=""col-sm-3 col-form-label"">Role:</label>
                                <div class=""col-sm-9 mr-auto"">
                                    <div class=""form-group"">
                                        <select class=""form-control"">
                                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a37c893e4a89f0ec684b2ab21a2249a8238d88ed15710", async() => {
                    WriteLiteral("Admin");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a37c893e4a89f0ec684b2ab21a2249a8238d88ed16768", async() => {
                    WriteLiteral("User");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                        </select>\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                            <!--End Input Edit user-->\r\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
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
</div>");
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
