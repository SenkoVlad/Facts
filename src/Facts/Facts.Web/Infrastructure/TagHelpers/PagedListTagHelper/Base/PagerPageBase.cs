using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Infrastructure.TagHelpers.PagedListTagHelper.Base
{
    public abstract class PagerPageBase
    {
        public PagerPageBase(string title, int value, bool isActive, bool isDisabled = false)
        {
            Title = title;
            Value = value;
            IsActive = isActive;
            IsDisabled = isDisabled;
        }

        public string Title { get;}
        public int Value { get;}
        public bool IsActive { get; set; }
        public bool IsDisabled { get; set; }
    }
}
