#pragma checksum "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "690fea20c96dfba6d7ccdca4bc4fe58c971c3d2e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Paging), @"mvc.1.0.view", @"/Views/Shared/_Paging.cshtml")]
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
#line 1 "C:\Users\dell\Documents\Folder 4 text\f7\Views\_ViewImports.cshtml"
using f7;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\dell\Documents\Folder 4 text\f7\Views\_ViewImports.cshtml"
using f7.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"690fea20c96dfba6d7ccdca4bc4fe58c971c3d2e", @"/Views/Shared/_Paging.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4b6c61aa004244664d24b1c54222e17d3e466163", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__Paging : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<f7.Models.PagingModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 9 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
  
    int currentPage  = Model.currentpage;
    int countPages   = Model.countpages;
    var generateUrl  = Model.generateUrl;



    if (currentPage > countPages)
      currentPage = countPages;

    if (countPages <= 1) return;


    int? preview = null;
    int? next = null;

    if (currentPage > 1)
        preview = currentPage - 1;

    if (currentPage < countPages)
        next = currentPage + 1;

    // Các trang hiện thị trong điều hướng
    List<int> pagesRanges = new List<int>();


    int delta = 5;             // Số trang mở rộng về mỗi bên trang hiện tại
    int remain = delta * 2;     // Số trang hai bên trang hiện tại

    pagesRanges.Add(currentPage);
    // Các trang phát triển về hai bên trang hiện tại
    for (int i = 1; i <= delta; i++)
    {
        if (currentPage + i  <= countPages) {
            pagesRanges.Add(currentPage + i);
            remain --;
        }

        if (currentPage - i >= 1) {
            pagesRanges.Insert(0, currentPage - i);
            remain --;
        }

    }
    // Xử lý thêm vào các trang cho đủ remain
    //(xảy ra ở đầu mút của khoảng trang không đủ trang chèn  vào)
    if (remain > 0) {
        if (pagesRanges[0] == 1) {
            for (int i = 1; i <= remain; i++)
            {
                if (pagesRanges.Last() + 1 <= countPages) {
                    pagesRanges.Add(pagesRanges.Last() + 1);
                }
            }
        }
        else {
            for (int i = 1; i <= remain; i++)
            {
                if (pagesRanges.First() - 1 > 1) {
                    pagesRanges.Insert(0, pagesRanges.First() - 1);
                }
            }
        }
    }


#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
            WriteLiteral("\n<ul class=\"pagination\">\n    <!-- Previous page link -->\n");
#nullable restore
#line 80 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
     if (preview != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"page-item\">\n            <a class=\"page-link\"");
            BeginWriteAttribute("href", " href=\"", 2060, "\"", 2088, 1);
#nullable restore
#line 83 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
WriteAttributeValue("", 2067, generateUrl(preview), 2067, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Trang trước</a>\n        </li>\n");
#nullable restore
#line 85 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"page-item disabled\">\n            <a class=\"page-link\" href=\"#\" tabindex=\"-1\" aria-disabled=\"true\">Trang trước</a>\n        </li>\n");
#nullable restore
#line 91 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    <!-- Numbered page links -->\n");
#nullable restore
#line 94 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
     foreach (var pageitem in pagesRanges)
    {
        if (pageitem != currentPage) {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"page-item\">\n                <a class=\"page-link\"");
            BeginWriteAttribute("href", " href=\"", 2487, "\"", 2516, 1);
#nullable restore
#line 98 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
WriteAttributeValue("", 2494, generateUrl(pageitem), 2494, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n                    ");
#nullable restore
#line 99 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
               Write(pageitem);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </a>\n            </li>\n");
#nullable restore
#line 102 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"page-item active\" aria-current=\"page\">\n                <a class=\"page-link\" href=\"#\">");
#nullable restore
#line 106 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
                                         Write(pageitem);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <span class=\"sr-only\">(current)</span></a>\n            </li>\n");
#nullable restore
#line 108 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n    <!-- Next page link -->\n");
#nullable restore
#line 113 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
     if (next != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"page-item\">\n            <a class=\"page-link\"");
            BeginWriteAttribute("href", " href=\"", 2938, "\"", 2963, 1);
#nullable restore
#line 116 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
WriteAttributeValue("", 2945, generateUrl(next), 2945, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Trang sau</a>\n        </li>\n");
#nullable restore
#line 118 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"page-item disabled\">\n            <a class=\"page-link\" href=\"#\" tabindex=\"-1\" aria-disabled=\"true\">Trang sau</a>\n        </li>\n");
#nullable restore
#line 124 "C:\Users\dell\Documents\Folder 4 text\f7\Views\Shared\_Paging.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<f7.Models.PagingModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
