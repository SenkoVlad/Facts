using Facts.Web.Infrastructure.TagHelpers.PagedListTagHelper.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Infrastructure.TagHelpers.PagedListTagHelper
{
    public class PagerPageDisabled : PagerPageBase
    {
        public PagerPageDisabled(string title, int value) : base(title, value, false, true)
        {
        }
    }
}
