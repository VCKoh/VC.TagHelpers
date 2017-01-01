using System.Threading.Tasks;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace VC.TagHelpers
{
    [HtmlTargetElement("pager-info", ParentTag = "pager")]
    public class PagerInfoTagHelper : TagHelper
    {
        private const string PagerInfoTemplateAttributeName = "pager-info-template";

        private const string DefaultPagerInfoTemplate =
            "Showing {{FirstItemOnPage}} to {{LastItemOnPage}} of {{TotalItemCount}} entries";

        public string Id { get; set; }

        public string ClassNames { get; set; }

        [HtmlAttributeName(PagerInfoTemplateAttributeName)]
        public string PagerInfoTemplate { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var pagerContext = (PagerContext) context.Items[typeof(PagerTagHelper)];

            var div = new TagBuilder("div");

            if (string.IsNullOrEmpty(Id))
                Id = $"{pagerContext.PagerId}_info";

            div.Attributes.Add("id", Id);

            if (string.IsNullOrEmpty(ClassNames))
                ClassNames = "pager_info";

            div.AddCssClass(ClassNames);

            if (string.IsNullOrEmpty(PagerInfoTemplate))
                PagerInfoTemplate = DefaultPagerInfoTemplate;

            var pagerInfo = GetPagerInfo(pagerContext);
            div.InnerHtml.Append(pagerInfo);

            //pagerContext.Info = div;
            //output.SuppressOutput();
            output.Content.AppendHtml(div);
        }

        private string GetPagerInfo(PagerContext pager)
        {
            var template = Handlebars.Compile(PagerInfoTemplate);

            return template(pager);
        }
    }
}