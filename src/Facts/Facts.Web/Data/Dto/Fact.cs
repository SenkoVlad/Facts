using Calabonga.EntityFrameworkCore.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Data.Dto
{
    public class Fact : Auditable
    {
        public string Content { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
