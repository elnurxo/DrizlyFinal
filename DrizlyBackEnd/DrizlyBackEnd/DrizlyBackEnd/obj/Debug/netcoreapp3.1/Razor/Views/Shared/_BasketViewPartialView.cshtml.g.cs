#pragma checksum "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36e1ff5fe9bb69b72d9f8c5d58426a914d763f26"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__BasketViewPartialView), @"mvc.1.0.view", @"/Views/Shared/_BasketViewPartialView.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"36e1ff5fe9bb69b72d9f8c5d58426a914d763f26", @"/Views/Shared/_BasketViewPartialView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5dd4a8a34180fe090d88641a04feaac3c1026e98", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__BasketViewPartialView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BasketViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("basket-remove-all"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "shop", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "removeallbasket", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
  
    decimal cardtotal = 0;
    var counter = 0;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36e1ff5fe9bb69b72d9f8c5d58426a914d763f265285", async() => {
                WriteLiteral(@"
    <!-- Toastr Css -->
    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css"" integrity=""sha512-vKMx8UnXk60zUwyUnUPM3HbQo8QfmNx7+ltw8Pm5zLusl1XIfwcxo8DbWCqMGKaWeNxWA8yrx5v3SaVpMvR3CA=="" crossorigin=""anonymous"" referrerpolicy=""no-referrer"" />
");
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
            WriteLiteral("\r\n");
#nullable restore
#line 10 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
 if (TempData["Success"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <input type=\"hidden\" id=\"success-toaster\"");
            BeginWriteAttribute("value", " value=\"", 476, "\"", 504, 1);
#nullable restore
#line 12 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
WriteAttributeValue("", 484, TempData["Success"], 484, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n");
#nullable restore
#line 13 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
}

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
 if (TempData["Warning"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <input type=\"hidden\" id=\"warning-toaster\"");
            BeginWriteAttribute("value", " value=\"", 596, "\"", 624, 1);
#nullable restore
#line 16 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
WriteAttributeValue("", 604, TempData["Warning"], 604, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n");
#nullable restore
#line 17 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""table-wrapper container"">
    <div class=""row"">
        <div class=""col-lg-12"">
            <table class=""table table-bordered table-responsive-sm"">
                <thead>
                    <tr>
                        <th scope=""col"" class=""image-th"">Images</th>
                        <th scope=""col"" class=""product-name-th"">Product</th>
                        <th scope=""col"" class=""product-price-th"">Unit Price</th>
                        <th scope=""col"" class=""total-price-th"">Total</th>
                        <th scope=""col"" class=""remove-th"">Remove</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 32 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
                     foreach (var item in Model.BasketItems)
                    {
                        counter += item.Count;

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td class=\"image-td\">\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "36e1ff5fe9bb69b72d9f8c5d58426a914d763f269654", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1559, "~/uploads/products/", 1559, 19, true);
#nullable restore
#line 37 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
AddHtmlAttributeValue("", 1578, item.Product.Image, 1578, 19, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </td>\r\n                            <td class=\"product-name-td\">");
#nullable restore
#line 39 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
                                                   Write(item.Product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td class=\"product-price-td\">$");
#nullable restore
#line 40 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
                                                      Write( (item.Product.DiscountPercent>0?(item.Product.SalePrice*(1-item.Product.DiscountPercent/100)):item.Product.SalePrice).ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td class=\"total-price-td\">$");
#nullable restore
#line 41 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
                                                    Write( ( (item.Product.DiscountPercent > 0 ? (item.Product.SalePrice * (1 - item.Product.DiscountPercent / 100)) : item.Product.SalePrice)*item.Count).ToString("0.00") );

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td class=\"remove-td\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36e1ff5fe9bb69b72d9f8c5d58426a914d763f2612768", async() => {
                WriteLiteral("<i class=\"fas fa-solid fa-trash\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 42 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
                                                                                                                                    WriteLiteral(item.Product.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</td>\r\n                        </tr>\r\n");
#nullable restore
#line 44 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
                        cardtotal += ((item.Product.DiscountPercent > 0 ? (item.Product.SalePrice * (1 - item.Product.DiscountPercent / 100)) : item.Product.SalePrice) * item.Count);
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                </tbody>
            </table>
        </div>
    </div>
    <div class=""row"">
        <div class=""col-lg-8 col-md-12"">
            <div class=""coupon-enter"">
                <input type=""text"" class=""coupon-input"" placeholder=""Coupon code"">
                <button class=""apply-coupon"">Apply Coupon</button>
            </div>
        </div>
        <div class=""col-lg-4 col-md-12"">
            <div class=""card-total-div"">
                <div class=""card-total-header"">
                    Cart Total
                </div>
                <div class=""card-total-item"">
                    <p class=""card-total-text"">Total</p>
                    <p class=""card-total-price"">$");
#nullable restore
#line 64 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
                                            Write(cardtotal.ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                </div>\r\n                <button type=\"submit\">Proceed To Checkout</button>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 3518, "\"", 3534, 1);
#nullable restore
#line 71 "C:\Users\LENOVO\Desktop\DrizlyFinal\DrizlyBackEnd\DrizlyBackEnd\DrizlyBackEnd\Views\Shared\_BasketViewPartialView.cshtml"
WriteAttributeValue("", 3526, counter, 3526, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"basket-counter-view\" />\r\n");
            WriteLiteral(@"<script src=""https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"" integrity=""sha512-VEd+nq25CkR676O+pLBnDW09R7VQX9Mdiij052gVCp5yVH3jGtH70Ho/UUv4mJDsEdTvqRCFZg0NKGiojGnUCw=="" crossorigin=""anonymous"" referrerpolicy=""no-referrer""></script>
<script>
        toastr.options = {
            ""closeButton"": false,
            ""debug"": false,
            ""newestOnTop"": false,
            ""progressBar"": false,
            ""positionClass"": ""toast-top-right"",
            ""preventDuplicates"": false,
            ""onclick"": null,
            ""showDuration"": ""200"",
            ""hideDuration"": ""350"",
            ""timeOut"": ""1000"",
            ""extendedTimeOut"": ""1000"",
            ""showEasing"": ""swing"",
            ""hideEasing"": ""linear"",
            ""showMethod"": ""fadeIn"",
            ""hideMethod"": ""fadeOut""
        }
        $(document).ready(function () {
            if ($(""#success-toaster"").length) {
                toastr[""success""]($(""#success-toaster"").val());
            }
    ");
            WriteLiteral("    });\r\n        $(document).ready(function () {\r\n            if ($(\"#warning-toaster\").length) {\r\n                toastr[\"warning\"]($(\"#warning-toaster\").val());\r\n            }\r\n        });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BasketViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
