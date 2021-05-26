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
            ViewData["search"] = search;
            ViewData["tag"] = tag;

            var index = pageId ?? 1;
            var operation = await _mediator.Send(new FactGetPagedRequest(index, tag, search), HttpContext.RequestAborted);
            if(operation.Ok && operation.Result.TotalPages < index)
            {
                return RedirectToAction(nameof(Index), new { tag, search, pageIndex = index });
            }
            return View(operation);
        } 

        public async Task<IActionResult> Show(Guid id, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            return View(await _mediator.Send(new FactGetByIdRequest(id), HttpContext.RequestAborted));
        }
        public async Task<IActionResult> Random()
        {
            return View(await _mediator.Send(new FactGetRandomRequest(), HttpContext.RequestAborted));
        }        
        public async Task<IActionResult> Rss(int count = 20)
        {
            return Content(await _mediator.Send(new FactRssRequest(count), HttpContext.RequestAborted));
        }
    }
}
