using Calabonga.UnitOfWork;
using Facts.Web.Data.Dto;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Facts.Web.Infrastructure.Services
{
    public interface IFactService
    {
        IEnumerable<Fact> GetLast(int count);
    }

    public class FactService : IFactService
    {
        private readonly IUnitOfWork unitOfWork;
        public FactService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Fact> GetLast(int count)
        {
            var result = unitOfWork.GetRepository<Fact>()
                             .GetAll(true)
                             .Include(x => x.Tags)
                             .OrderBy(x => x.CreatedAt)
                             .Take(count)
                             .AsEnumerable();

            return result;
        }
    }
}
