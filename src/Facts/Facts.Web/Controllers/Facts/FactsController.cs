using Facts.Web.Controllers.Facts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Controllers.Facts
{
    public class FactsController : Controller
    {
        private IMediator _mediator;
        public FactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(int? pageId, string tag, string search)
        {
            var operationResult = await _mediator.Send(new FactGetPagedRequest(pageId ?? 0, tag, search), HttpContext.RequestAborted);
            return View(operationResult);
        }
    }
}
