#pragma checksum "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7083cf2e07cf0ae9fa8c40d43384318311ab15bc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Item_Views_ItemIndex), @"mvc.1.0.view", @"/Areas/Item/Views/ItemIndex.cshtml")]
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
#line 1 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\_ViewImports.cshtml"
using f7;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\_ViewImports.cshtml"
using f7.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Logging;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7083cf2e07cf0ae9fa8c40d43384318311ab15bc", @"/Areas/Item/Views/ItemIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ffd1480a9ccb16c79889049d63c48757d66f4ca3", @"/Areas/Item/Views/_ViewImports.cshtml")]
    public class Areas_Item_Views_ItemIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<f7.Models.ItemModels>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_PagingPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
  
    ViewData["Title"] = "Index";
    var pagingModel = TempData["PagingModel"] as f7.Models.PagingModel;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"" id=""modal-container"">
    <button id=""launch-btn"" type=""button"" class=""btn btn-primary"" style=""display: none"" data-bs-toggle=""modal""
        data-bs-target=""#exampleModal"">while u steel hear?</button>
    <div></div>
</div>

<div class=""container"">
    <div class=""table-responsive-lg"">
        <div class=""d-flex justify-content-end"">
            <a class=""btn btn-primary add-item-btn m-0"">Thêm</a>
        </div>
        <table class=""table table-striped table-light caption-top"" id=""item-table"">
            <thead>
                <tr>
                    <th scope=""col""");
            BeginWriteAttribute("class", " class=\"", 773, "\"", 781, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        ");
#nullable restore
#line 23 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                   Write(Html.DisplayNameFor(model => model.ItemName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th scope=\"col\" style=\"max-width: 200px\">\r\n                        ");
#nullable restore
#line 26 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                   Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th scope=\"col\"");
            BeginWriteAttribute("class", " class=\"", 1082, "\"", 1090, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        ");
#nullable restore
#line 29 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                   Write(Html.DisplayNameFor(model => model.Unit));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th scope=\"col\"");
            BeginWriteAttribute("class", " class=\"", 1223, "\"", 1231, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        ");
#nullable restore
#line 32 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                   Write(Html.DisplayNameFor(model => model.SellingPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th scope=\"col\"");
            BeginWriteAttribute("class", " class=\"", 1372, "\"", 1380, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        ");
#nullable restore
#line 35 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                   Write(Html.DisplayNameFor(model => model.Image));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th scope=\"col\"");
            BeginWriteAttribute("class", " class=\"", 1514, "\"", 1522, 0);
            EndWriteAttribute();
            WriteLiteral("></th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
#nullable restore
#line 41 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 45 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                       Write(Html.DisplayFor(modelItem => item.ItemName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td style=\"max-width: 200px\">\r\n                            ");
#nullable restore
#line 48 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 51 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Unit));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 54 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                       Write(Html.DisplayFor(modelItem => item.SellingPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 57 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Image));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            <a class=\"update-btn btn btn-secondary\">Sửa</a>\r\n");
            WriteLiteral("                            <a class=\"delete-btn btn btn-danger\">Xóa</a>\r\n                            <input type=\"hidden\" name=\"id\"");
            BeginWriteAttribute("value", " value=\"", 2721, "\"", 2741, 1);
#nullable restore
#line 63 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
WriteAttributeValue("", 2729, item.ItemId, 2729, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 66 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n    <div class=\"d-flex justify-content-center mt-5\" id=\"cart-icon-wrapper\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "7083cf2e07cf0ae9fa8c40d43384318311ab15bc10343", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 71 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = pagingModel;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n\r\n</div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n<script type=\"text/javascript\">\r\n    $(document).ready(function () {\r\n\r\n        $(\".update-btn\").on(\'click\', function () {\r\n            var text = $(this).nextAll(\"input\").first().serialize();\r\n            $.ajax({\r\n                url: \'");
#nullable restore
#line 82 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                 Write(Url.Action("getedit", "item"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                type: ""GET"",
                data: text,
                success: function (d, s, x) {
                    $(""#launch-btn + div"").html(d);
                    $(""#launch-btn"").click();
                },
                error: function (d, s, x) {
                    alert(x.responseText);
                }
            });
        });

        $("".add-item-btn"").click(function () {
            $.ajax({
                type: ""GET"",
                url: '");
#nullable restore
#line 98 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                 Write(Url.Action("GetCreate", "item"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                contentType: ""text/plain; charset=UTF-8"",
                success: function (result, status, xhr) {
                    var modalWrapper = $(""#launch-btn + div"");
                    modalWrapper.html(result);
                    $(""#launch-btn"").click();
                },
                error: function (result, status, xhr) {
                    alert(status);
                }
            });
        });

        $(""#modal-container"").on('click', ""#create-btn"", function () {
            var form = $(""form[name=create-item]"");
            var form_encoded = form.serialize();
            alert(form_encoded);
            $.ajax({
                url: '");
#nullable restore
#line 116 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                 Write(Url.Action("PostCreate", "item"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                type: ""POST"",
                data: form.serialize(),
                success: function (data) {
                    alert(""them thanh cong"");
                    $(""exampleModal"").focusout();
                },
                error: function (d, s, x) {
                    alert(x.responseText);
                }
            });
        });

        $(""#modal-container"").on('click', ""#update-btn"", function () {
            var serializedData = $(""form[name=update-item]"").serialize();
            $.ajax({
                url: '");
#nullable restore
#line 132 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                 Write(Url.Action("PostEdit", "Item"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                type: ""POST"",
                data: serializedData,
                success: function (d, s, x) {
                    $(""modaldialog"").focusout();
                    alert('Đã sửa');
                },
                error: function (d, s, x) {
                    alert(x.responseText);
                }
            });
        });

        $("".delete-btn"").click(function () {
            var serializedData = $(this).nextAll(""input"").serialize();
            if (confirm(""Bạn có chắc muốn xóa?"")) {
                $.ajax({
                    url: '");
#nullable restore
#line 149 "C:\Users\dell\Documents\Folder 4 text\f7\Areas\Item\Views\ItemIndex.cshtml"
                     Write(Url.Action("postdelete", "Item"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                    type: ""POST"",
                    data: serializedData,
                    success: function (d, s, x) {
                        alert('Đã xóa');
                    },
                    error: function (d, s, x) {
                        alert(x.responseText);
                    }
                });
            }
        });

    });
</script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<f7.Models.ItemModels>> Html { get; private set; }
    }
}
#pragma warning restore 1591