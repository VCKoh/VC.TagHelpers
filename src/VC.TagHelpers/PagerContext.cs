﻿using Microsoft.AspNetCore.Html;

namespace VC.TagHelpers
{
    public class PagerContext
    {
        //public PagerTagHelper Pager { get; set; }
        public string PagerId { get; set; }
        public int FirstItemOnPage { get; set; }
        public int LastItemOnPage { get; set; }
        public int TotalItemCount { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }

        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }

        public IHtmlContent Info { get; set; }
        public IHtmlContent Paginator { get; set; }
    }
}