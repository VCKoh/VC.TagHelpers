using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace VC.TagHelpers
{
    [HtmlTargetElement("pager")]
    //[RestrictChildren("pager-info", "pager-paginator")]
    public class PagerTagHelper : TagHelper
    {
        private const string PagerPageNumberAttributeName = "pager-pagenumber";
        private const string PagerPageSizeAttributeName = "pager-pagesize";
        private const string PagerTotalItemCountAttributeName = "pager-totalitemcount";

        /// <summary>
        ///     The Id of the pager
        /// </summary>
        public string Id { get; set; }

        public string ClassNames { get; set; }

        public int PageCount { get; protected set; }

        [HtmlAttributeName(PagerPageNumberAttributeName)]
        public int PageNumber { get; set; }

        [HtmlAttributeName(PagerPageSizeAttributeName)]
        public int PageSize { get; set; }

        [HtmlAttributeName(PagerTotalItemCountAttributeName)]
        public int TotalItemCount { get; set; }

        public int FirstItemOnPage { get; protected set; }
        public int LastItemOnPage { get; protected set; }

        public bool HasPreviousPage { get; protected set; }
        public bool HasNextPage { get; protected set; }

        public bool IsFirstPage { get; protected set; }
        public bool IsLastPage { get; protected set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(Id))
                Id = "pager1";

            PageCount = TotalItemCount > 0
                ? (int) Math.Ceiling(TotalItemCount/(double) PageSize)
                : 0;

            PageNumber = PageNumber == int.MaxValue ? PageCount : PageNumber;
            PageNumber = PageNumber < 1 ? 1 : PageNumber;

            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;

            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;

            FirstItemOnPage = TotalItemCount == 0
                ? TotalItemCount
                : (PageNumber - 1)*PageSize + 1;

            var numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount
                ? TotalItemCount
                : numberOfLastItemOnPage;

            if (FirstItemOnPage > TotalItemCount)
            {
                FirstItemOnPage = 0;
                LastItemOnPage = 0;
            }

            var pagerContext = new PagerContext
            {
                PagerId = Id,
                FirstItemOnPage = FirstItemOnPage,
                LastItemOnPage = LastItemOnPage,
                TotalItemCount = TotalItemCount,
                PageNumber = PageNumber,
                PageSize = PageSize,
                PageCount = PageCount,
                HasPreviousPage = HasPreviousPage,
                HasNextPage = HasNextPage,
                IsFirstPage = IsFirstPage,
                IsLastPage = IsLastPage
            };

            context.Items.Add(typeof(PagerTagHelper), pagerContext);

            output.TagName = "div";

            output.Attributes.SetAttribute("id", Id);

            if (string.IsNullOrEmpty(ClassNames))
                ClassNames = "pager_container";

            output.Attributes.SetAttribute("class", ClassNames);

            //if (pagerContext.Info != null)
            //{
            //    output.Content.AppendHtml(pagerContext.Info);
            //}

            //if (pagerContext.Paginator != null)
            //{
            //    output.Content.AppendHtml(pagerContext.Paginator);
            //}
        }
    }
}