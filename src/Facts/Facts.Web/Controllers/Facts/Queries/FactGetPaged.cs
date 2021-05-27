using AutoMapper;

using Calabonga.AspNetCore.Controllers.Base;

using Calabonga.OperationResults;
using Calabonga.UnitOfWork;

using Facts.Web.Data.Dto;
using Facts.Web.ViewModels;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Calabonga.AspNetCore.Controllers;
using Calabonga.PredicatesBuilder;
using System;
using System.Linq.Expressions;

namespace Facts.Web.Controllers.Facts.Queries
{
    public class FactGetPagedRequest : OperationResultRequestBase<IPagedList<FactViewModel>>
    {
        public int PageIndex { get; }
        public string? Tag { get; }
        public string? Search { get;  }
        public int PageSize { get; set; } = 20;

        public FactGetPagedRequest(int pageIndex, string? tag, string? search)
        {
            this.PageIndex = pageIndex - 1 < 0 ? 0 : pageIndex -1;
            Tag = tag;
            Search = search;
        }
    }

    public class FactGetPagedRequestHandler : OperationResultRequestHandlerBase<FactGetPagedRequest, IPagedList<FactViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FactGetPagedRequestHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public override async Task<OperationResult<IPagedList<FactViewModel>>> Handle(FactGetPagedRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<IPagedList<FactViewModel>>();
            var predicate = CreatePreducate(request);

            var items = await _unitOfWork.GetRepository<Fact>()
                .GetPagedListAsync(
                    predicate: predicate,
                    include: i => i.Include(x => x.Tags),
                    orderBy: o => o.OrderByDescending(x => x.CreatedAt),
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    cancellationToken: cancellationToken);

            var mapped = _mapper.Map<IPagedList<FactViewModel>>(items);
            operation.AddSuccess("Success");

            operation.Result = mapped;
            return operation;
        } 
            
        private Expression<Func<Fact, bool>> CreatePreducate(FactGetPagedRequest request)
        {
            var predicate = PredicateBuilder.True<Fact>();

            if (!string.IsNullOrWhiteSpace(request.Search))
                predicate = predicate.And(x => x.Content.Contains(request.Search));

            if (!string.IsNullOrWhiteSpace(request.Tag))
                predicate = predicate.And(x => x.Tags.Select(x => x.Name).Contains(request.Tag));

            return predicate;
        }
    }
}
