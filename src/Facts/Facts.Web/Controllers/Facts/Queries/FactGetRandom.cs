using AutoMapper;
using Calabonga.AspNetCore.Controllers;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.UnitOfWork;
using Facts.Web.Data.Dto;
using Facts.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Facts.Web.Controllers.Facts.Queries
{
    public class FactGetRandomRequest : RequestBase<FactViewModel> { }

    public class FactGetRandomRequestHandler : RequestHandlerBase<FactGetRandomRequest, FactViewModel>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FactGetRandomRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public override async Task<FactViewModel> Handle(FactGetRandomRequest request, CancellationToken cancellationToken)
        {
            var fact =  await unitOfWork.GetRepository<Fact>()
                                 .GetAll(true)
                                 .OrderBy(x => Guid.NewGuid())
                                 .FirstOrDefaultAsync(cancellationToken);

            return fact == null ?
                new FactViewModel { Content = "Not Data" } :
                mapper.Map<FactViewModel>(fact);
        }
    }
}
