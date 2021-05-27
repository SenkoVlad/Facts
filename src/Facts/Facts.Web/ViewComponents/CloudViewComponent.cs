using Facts.Web.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Facts.Web.ViewComponents
{
    public class CloudViewComponent : ViewComponent
    {
        private readonly ITagService tagService;

        public CloudViewComponent(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tags = await tagService.GetClouds();
            return View(tags);
        }
    }
}
