#pragma checksum "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c6e8c3ecc1a32f6d19aa0b9d2daaade3b077494d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__SearchProductPartialView), @"mvc.1.0.view", @"/Views/Shared/_SearchProductPartialView.cshtml")]
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
#line 1 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\_ViewImports.cshtml"
using DrizlyBackEnd;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\_ViewImports.cshtml"
using DrizlyBackEnd.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\_ViewImports.cshtml"
using DrizlyBackEnd.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c6e8c3ecc1a32f6d19aa0b9d2daaade3b077494d", @"/Views/Shared/_SearchProductPartialView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5dd4a8a34180fe090d88641a04feaac3c1026e98", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__SearchProductPartialView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Product>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "shop", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "detail", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("d-flex justify-content-between"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml"
   int count = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("<ul>\r\n");
#nullable restore
#line 4 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml"
     foreach (var item in Model)
    {
        count++;

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6e8c3ecc1a32f6d19aa0b9d2daaade3b077494d5153", async() => {
                WriteLiteral("\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "c6e8c3ecc1a32f6d19aa0b9d2daaade3b077494d5424", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 298, "~/uploads/products/", 298, 19, true);
#nullable restore
#line 9 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml"
AddHtmlAttributeValue("", 317, item.Image, 317, 11, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                <span class=\"font-weight-bolder\">");
#nullable restore
#line 10 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml"
                                            Write(item.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</span>\r\n                <span>\r\n");
#nullable restore
#line 12 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml"
                     if (item.DiscountPercent > 0)
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                         <span class=\"regular-price font-weight-bold text-danger\">");
#nullable restore
#line 14 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml"
                                                                              Write((item.SalePrice * (1 - item.DiscountPercent / 100)).ToString("0.00"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</span>\r\n");
#nullable restore
#line 15 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <span class=\"regular-price font-weight-bolder text-danger\">");
#nullable restore
#line 18 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml"
                                                                              Write(item.SalePrice);

#line default
#line hidden
#nullable disable
                WriteLiteral("</span>\r\n");
#nullable restore
#line 19 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml"
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral("                </span>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 8 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml"
                                                           WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 245, "search-list-item-", 245, 17, true);
#nullable restore
#line 8 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml"
AddHtmlAttributeValue("", 262, count, 262, 6, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </li>\r\n");
#nullable restore
#line 23 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_SearchProductPartialView.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</ul>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Product>> Html { get; private set; }
    }
}
#pragma warning restore 1591
