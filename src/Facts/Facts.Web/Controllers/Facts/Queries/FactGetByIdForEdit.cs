using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Facts.Web.Data.Dto;
using Facts.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Facts.Web.Controllers.Facts.Queries
{
    public class FactGetByIdForEditRequest : OperationResultRequestBase<FactEditViewModel> 
    {
        public FactGetByIdForEditRequest(Guid Id, string ReturnUrl = null!)
        {
            this.Id = Id;
            this.ReturnUrl = ReturnUrl;
        }

        public Guid Id { get; }
        public string ReturnUrl { get; }
    }

    public class FactGetByIdForEditRequestHandler : OperationResultRequestHandlerBase<FactGetByIdForEditRequest, FactEditViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FactGetByIdForEditRequestHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public override async Task<OperationResult<FactEditViewModel>> Handle(FactGetByIdForEditRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<FactEditViewModel>();

            var entity = await _unitOfWork.GetRepository<Fact>()
                .GetFirstOrDefaultAsync(
                    s => new FactEditViewModel
                    {
                        Content = s.Content,
                        Id = s.Id,
                        ReturnUrl = request.ReturnUrl,
                        Tags = s.Tags!.Select(x => x.Name).ToList(),
                        TotalTags = s.Tags!.Count

                    },
                    x => x.Id == request.Id);
            if (entity is not null)
            {
                operation.Result = entity;
                return operation;
            }

            operation.AddError(new MicroserviceNotFoundException($"{nameof(Fact)} not found with Id: {request.Id}"));
            return operation;
        }
    }
}
