using Calabonga.EntityFrameworkCore.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Data.Dto
{
    public class Tag : Identity
    {
        public string Name { get; set; }
        public ICollection<Fact> Facts { get; set; }
    }
}
