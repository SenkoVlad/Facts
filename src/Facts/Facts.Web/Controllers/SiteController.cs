using Facts.Web.Mediatr;
using Facts.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Facts.Web.Controllers
{
    public class SiteController : Controller
    {
        private readonly ILogger<SiteController> _logger;
        private IMediator _mediator;

        public SiteController(ILogger<SiteController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(int pageId, string tag, string search)
        {
            ViewData["pageId"] = pageId;
            ViewData["tag"] = tag;
            ViewData["search"] = search;
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            await _mediator.Publish(new ErrorNotification("Privacy test for notification"), HttpContext.RequestAborted);
            await _mediator.Publish(new FeedbackNotification("Privacy test for feedback"), HttpContext.RequestAborted);

            return View();
        }

        public IActionResult About() => View();

        public IActionResult Random() => View();

        public IActionResult Cloud() => View();

        public IActionResult Feedback() => View();

        public IActionResult Rss() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
