using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Infrastructure.TagHelpers.PagedListTagHelper
{
    public class PagerContext
    {
        public int GroupIndex { get; set; }
        public int MinPage { get; set; }
        public int MaxPage { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
