using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace VC.TagHelpers
{
    [HtmlTargetElement("pager-paginator", ParentTag = "pager")]
    public class PagerPaginatorTagHelper : TagHelper
    {
        private const string FirstTextAttributeName = "pager-paginator-first-text";
        private const string PreviousTextAttributeName = "pager-paginator-previous-text";
        private const string NextTextAttributeName = "pager-paginator-next-text";
        private const string LastTextAttributeName = "pager-paginator-last-text";
        private const string UrlAttributeName = "pager-paginator-url";

        private const string DefaultFirstText = "First";
        private const string DefaultPreviousText = "Previous";
        private const string DefaultNextText = "Next";
        private const string DefaultLastText = "Last";

        private PagerContext _pagerContext;

        public string Id { get; set; }

        public string ClassNames { get; set; }

        [HtmlAttributeName(FirstTextAttributeName)]
        public string FirstText { get; set; }

        [HtmlAttributeName(PreviousTextAttributeName)]
        public string PreviousText { get; set; }

        [HtmlAttributeName(NextTextAttributeName)]
        public string NextText { get; set; }

        [HtmlAttributeName(LastTextAttributeName)]
        public string LastText { get; set; }

        [HtmlAttributeName(UrlAttributeName)]
        public Func<int, string> GetPageUrl { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            _pagerContext = (PagerContext) context.Items[typeof(PagerTagHelper)];

            var div = new TagBuilder("div");

            if (string.IsNullOrEmpty(Id))
                Id = $"{_pagerContext.PagerId}_paginator";

            div.Attributes.Add("id", Id);

            if (string.IsNullOrEmpty(ClassNames))
                ClassNames = "pager_paginator";

            div.AddCssClass(ClassNames);

            if (string.IsNullOrEmpty(FirstText))
                FirstText = DefaultFirstText;
            if (string.IsNullOrEmpty(PreviousText))
                PreviousText = DefaultPreviousText;
            if (string.IsNullOrEmpty(NextText))
                NextText = DefaultNextText;
            if (string.IsNullOrEmpty(LastText))
                LastText = DefaultLastText;

            div.InnerHtml.AppendHtml(GetFirst());
            div.InnerHtml.AppendHtml(GetPrevious());
            div.InnerHtml.AppendHtml(GetNext());
            div.InnerHtml.AppendHtml(GetLast());

            //_pagerContext.Paginator = div;
            //output.SuppressOutput();
            output.Content.AppendHtml(div);
        }

        private TagBuilder GetFirst()
        {
            const int targetPageNumber = 1;
            var link = new TagBuilder("a");
            link.InnerHtml.Append(FirstText);

            link.Attributes.Add("id", Id + "_first");
            link.AddCssClass("pager_paginator_button");

            if (_pagerContext.IsFirstPage)
                link.AddCssClass("disabled");
            else
                link.Attributes["href"] = GetPageUrl(targetPageNumber);

            return link;
        }

        private TagBuilder GetPrevious()
        {
            var targetPageNumber = _pagerContext.PageNumber - 1;
            var link = new TagBuilder("a");
            link.InnerHtml.Append(PreviousText);
            link.Attributes.Add("id", Id + "_previous");
            link.Attributes["rel"] = "prev";
            link.AddCssClass("pager_paginator_button");
            link.AddCssClass("previous");

            if (!_pagerContext.HasPreviousPage)
                link.AddCssClass("disabled");
            else
                link.Attributes["href"] = GetPageUrl(targetPageNumber);

            return link;
        }

        private TagBuilder GetNext()
        {
            var targetPageNumber = _pagerContext.PageNumber + 1;
            var link = new TagBuilder("a");
            link.InnerHtml.Append(NextText);
            link.Attributes.Add("id", Id + "_next");
            link.Attributes["rel"] = "next";
            link.AddCssClass("pager_paginator_button");
            link.AddCssClass("next");

            if (!_pagerContext.HasNextPage)
                link.AddCssClass("disabled");
            else
                link.Attributes["href"] = GetPageUrl(targetPageNumber);

            return link;
        }

        private TagBuilder GetLast()
        {
            var targetPageNumber = _pagerContext.PageCount;
            var link = new TagBuilder("a");
            link.InnerHtml.Append(LastText);
            link.Attributes.Add("id", Id + "_last");
            link.AddCssClass("pager_paginator_button");

            if (_pagerContext.IsLastPage)
                link.AddCssClass("disabled");
            else
                link.Attributes["href"] = GetPageUrl(targetPageNumber);

            return link;
        }
    }
}