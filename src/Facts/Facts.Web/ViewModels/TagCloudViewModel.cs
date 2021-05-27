using System;

namespace Facts.Web.ViewModels
{
    public class TagCloudViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? CssClass { get; set; }
        public int Total { get; set; }
    }
}
