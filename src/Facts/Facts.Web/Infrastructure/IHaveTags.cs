using System.Collections.Generic;

namespace Facts.Web.Infrastructure
{
    public interface IHaveTags
    {
        List<string>? Tags { get; set; }
    }
}