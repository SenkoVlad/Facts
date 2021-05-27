using Calabonga.UnitOfWork;
using Facts.Web.Data.Dto;
using Facts.Web.Infrastructure.Helpers;
using Facts.Web.ViewModels;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<TagCloudViewModel>> GetClouds()
        {
            var tags = await unitOfWork.GetRepository<Tag>()
                                 .GetAll(s => new TagCloudViewModel
                                 {
                                     CssClass = "",
                                     Id = Guid.NewGuid(),
                                     Name = s.Name,
                                     Total = s.Facts == null ? 0 : s.Facts.Count
                                 }, true)
                                 .ToListAsync();

            return TagCloudHelper.Generate(tags, 10);
        }
    }
    public interface ITagService
    {
        public Task<List<TagCloudViewModel>> GetClouds();
    }
}


