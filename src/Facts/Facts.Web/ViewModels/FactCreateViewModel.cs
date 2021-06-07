using Facts.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Facts.Web.ViewModels
{
    public class FactCreateViewModel : IHaveTags
    {
        /// <summary>
        /// Content for editing
        /// </summary>
        [Display(Name = "Содержание факта")]
        public string? Content { get; set; }

        public List<string>? Tags { get; set; }

        [Range(1, 8, ErrorMessage = "Требуется от 1 до 8 меток")]
        public int TotalTags { get; set; }
    }
}
