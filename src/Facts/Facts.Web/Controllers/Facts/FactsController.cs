using Facts.Web.Controllers.Facts.Command;
using Facts.Web.Controllers.Facts.Queries;
using Facts.Web.Infrastructure;
using Facts.Web.Infrastructure.Services;
using Facts.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facts.Web.Controllers.Facts
{
    public class FactsController : Controller
    {
        private IMediator _mediator;
        private ITagService _tagService;
        public FactsController(IMediator mediator, ITagService tagService)
        {
            _mediator = mediator;
            _tagService = tagService;
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

        #region Edit
        [Authorize(Roles = AppData.AdministratorRoleName)]
        public async Task<IActionResult> Edit(Guid id, string returnUrl)
        {
            var operationResult = await _mediator.Send(new FactGetByIdForEditRequest(id, returnUrl));
            if (operationResult.Ok)
            {
                return View(operationResult.Result);
            }

            return RedirectToAction("Error", "Site", new
            {
                code = 404
            });
        }

        [HttpPost]
        [Authorize(Roles = AppData.AdministratorRoleName)]
        public async Task<IActionResult> Edit(FactEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operationResult = await _mediator.Send(new FactUpdateRequest(model));
                if (operationResult.Ok)
                {
                    return string.IsNullOrEmpty(model.ReturnUrl)
                        ? RedirectToAction("Index", "Facts")
                        : Redirect(model.ReturnUrl);
                }
            }

            return View(model);
        }
        #endregion

        #region Add
        [Authorize(Roles = AppData.AdministratorRoleName)]
        public IActionResult Add()
        {
            var model = new FactCreateViewModel
            {
                Tags = new List<string>()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = AppData.AdministratorRoleName)]
        public async Task<IActionResult> Add(FactCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var operationResult = await _mediator.Send(new FactAddRequest(model));
                if (operationResult.Ok)
                {
                    return RedirectToAction("Index", "Facts");
                }
                ModelState.AddModelError("", operationResult.Exception.GetBaseException().Message);
            }

            return View(model);
        }
        #endregion

        public IActionResult Cloud() =>  View();
    }
}
