using Calabonga.EntityFrameworkCore.Entities.Base;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Data.Dto
{
    public class Fact : Auditable
    {
        public string Content { get; set; } = null!;
        public ICollection<Tag>? Tags { get; set; }
    }
}
