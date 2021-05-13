using Facts.Web.Infrastructure.TagHelpers.PagedListTagHelper.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Infrastructure.TagHelpers.PagedListTagHelper
{
    public class PagerPageActive : PagerPageBase
    {
        public PagerPageActive(string title, int value) : base(title, value, true, false)
        {
        }
    }
}
