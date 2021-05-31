using Calabonga.UnitOfWork;
using Facts.Contracts;
using Facts.Web.Data.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Infrastructure.Services
{
    public class TagSearchService : ITagSearchService
    {
        private readonly IUnitOfWork unitOfWork;

        public TagSearchService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public List<string> SearchTags(string retm)
        {
            return  unitOfWork.GetRepository<Tag>()
                                   .GetAll(
                                        s => s.Name,
                                        x => x.Name.ToLower().StartsWith(retm.ToLower()),
                                        true)
                                   .ToList();
        }
    }
}
