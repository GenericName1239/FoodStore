using FoodStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageInfo PageModel { get; set; }
        string PageAction { get; set; }
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        //css properties
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; } 
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder resultTag = new TagBuilder("div");

            for(int i=1; i<=PageModel.TotalPages; i++)
            {
                TagBuilder anchorTag = new TagBuilder("a");
                PageUrlValues["pageIndx"] = i;
                anchorTag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

                if(PageClassesEnabled)
                {
                    anchorTag.AddCssClass(PageClass);
                    anchorTag.AddCssClass(i != PageModel.CurrentPage ? PageClassNormal : PageClassSelected);
                }

                anchorTag.InnerHtml.Append(i.ToString());
                resultTag.InnerHtml.AppendHtml(anchorTag);
            }

            output.Content.AppendHtml(resultTag.InnerHtml);
        }
    }
}
