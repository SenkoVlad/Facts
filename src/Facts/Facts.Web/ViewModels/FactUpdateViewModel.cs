using Facts.Web.Data.Dto;
using System;
using System.Collections.Generic;

namespace Facts.Web.ViewModels
{
    public class FactUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
