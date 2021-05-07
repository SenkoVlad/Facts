using Facts.Web.Data;
using Facts.Web.Data.Dto;
using Facts.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Controllers
{
    public class SiteController : Controller
    {
        private readonly ILogger<SiteController> _logger;

        public SiteController(ILogger<SiteController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(int pageId, string tag, string search)
        {
            ViewData["pageId"] = pageId;
            ViewData["tag"] = tag;
            ViewData["search"] = search;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
