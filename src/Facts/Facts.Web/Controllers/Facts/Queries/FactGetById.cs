using AutoMapper;
using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Facts.Web.Data.Dto;
using Facts.Web.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Facts.Web.Controllers.Facts.Queries
{
    public class FactGetByIdRequest : OperationResultRequestBase<FactViewModel>
    {
        public Guid Id { get; set; }
        public FactGetByIdRequest(Guid id)
        {
            Id = id;
        }
    }

    public class FactGetByIdRequestHandler : OperationResultRequestHandlerBase<FactGetByIdRequest, FactViewModel>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public FactGetByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async override Task<OperationResult<FactViewModel>> Handle(FactGetByIdRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<FactViewModel>();
            operation.AppendLog("Searcing fact in DB");

            var entity = await _unitOfWork.GetRepository<Fact>()
                                          .GetFirstOrDefaultAsync(
                                                predicate: x => x.Id == request.Id,
                                                include: i => i.Include(x => x.Tags));
            if(entity is null)
            {
                operation.AddWarning($"Fact with id {request.Id} isn't found");
                return operation;
            }
            operation.AppendLog("Fact was found. Mapping...");
            operation.Result = _mapper.Map<FactViewModel>(entity);
            operation.AppendLog("Fact was returnen to UI...");
            return operation;
        }
    }
}
