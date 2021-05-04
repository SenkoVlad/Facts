using Facts.Web.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.ViewModels
{
    public class FactCreateViewModel
    {
        public string Content { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
