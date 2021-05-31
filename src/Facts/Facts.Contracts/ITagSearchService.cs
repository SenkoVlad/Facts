using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facts.Contracts
{
    public interface ITagSearchService
    {
        List<string> SearchTags(string retm);
    }
}
