
#VC TagHelpers

[VC TagHelpers Samples](http://vc-taghelpers.azurewebsites.net/) running on Azure.

VC Tag Helpers are available on [Nuget](https://www.nuget.org/packages/VC.TagHelpers).

`Install-Package VC.TagHelpers`

### Add Tag Helpers

Add the @addTagHelper directive to cshtml or globally in the Views/_ViewImports.cshtml file:

`@addTagHelper "*, VC.TagHelpers"`

### Configure Tag Helpers

Example 1:
      
	<pager pager-pagenumber="1" pager-pagesize="10" pager-totalitemcount="100">
	    <pager-info></pager-info>
	    <pager-paginator pager-paginator-url="@getPageUrl"></pager-paginator>
	</pager>

will produce the following html:

	<div id="pager1" class="pager_container">
		<pager-info>
			<div class="pager_info" id="pager1_info">Showing 1 to 10 of 100 entries</div>
		</pager-info>
		<pager-paginator>
			<div class="pager_paginator" id="pager1_paginator">
				<a class="disabled pager_paginator_button" id="pager1_paginator_first">First</a>
				<a class="disabled previous pager_paginator_button" id="pager1_paginator_previous" rel="prev">Previous</a>
				<a class="next pager_paginator_button" href="/?page=2" id="pager1_paginator_next" rel="next">Next</a>
				<a class="pager_paginator_button" href="/?page=10" id="pager1_paginator_last">Last</a>
			</div>
		</pager-paginator>
	</div>

Example 2:

    <pager id="pager2" pager-pagenumber="1" pager-pagesize="10" pager-totalitemcount="100">
        <pager-paginator pager-paginator-first-text="<<" pager-paginator-previous-text="<"
                         pager-paginator-next-text=">" pager-paginator-last-text=">>"
                         pager-paginator-url="@getPageUrl">
        </pager-paginator>
        <span class="text-center col-xs-6">You can put some content here</span>
        <pager-info pager-info-template="{{FirstItemOnPage}} to {{LastItemOnPage}} of {{TotalItemCount}}"></pager-info>
    </pager>

will produce the following html:

	<div id="pager2" class="pager_container">
		<pager-paginator>
			<div class="pager_paginator" id="pager2_paginator">
				<a class="disabled pager_paginator_button" id="pager2_paginator_first">&lt;&lt;</a>
				<a class="disabled previous pager_paginator_button" id="pager2_paginator_previous" rel="prev">&lt;</a>
				<a class="next pager_paginator_button" href="/?page=2" id="pager2_paginator_next" rel="next">&gt;</a>
				<a class="pager_paginator_button" href="/?page=10" id="pager2_paginator_last">&gt;&gt;</a>
			</div>
		</pager-paginator>
		<span class="text-center col-xs-6">You can put some content here</span>
		<pager-info>
			<div class="pager_info" id="pager2_info">1 to 10 of 100</div>
		</pager-info>
	</div>
