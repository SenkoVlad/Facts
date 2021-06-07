using Calabonga.UnitOfWork;
using Facts.Web.Data.Dto;
using Facts.Web.Infrastructure.Helpers;
using Facts.Web.ViewModels;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
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

            return FactHelper.Generate(tags, 10);
        }

        public async Task ProcessTagsAsync(IHaveTags viewModel, Fact fact, CancellationToken cancellationToken)
        {
            if (viewModel?.Tags is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (fact == null)
            {
                throw new ArgumentNullException(nameof(fact));
            }

            fact.Tags ??= new Collection<Tag>();

            var tagRepository = unitOfWork.GetRepository<Tag>();

            var afterEdit = viewModel.Tags!.ToArray();
            var oldArray = tagRepository
                           .GetAll(
                               x => x.Name.ToLower(),
                               x => x.Facts!.Select(p => p.Id).Contains(fact.Id),
                               null)
                           .ToArray();

            var (toCreate, toDelete) = new FactHelper().FindDifference(oldArray, afterEdit);

            if (toDelete.Any())
            {
                foreach (var name in toDelete)
                {
                    var tag = await tagRepository.GetFirstOrDefaultAsync(predicate: x => x.Name.ToLower() == name, disableTracking: false);
                    if (tag == null)
                    {
                        continue;
                    }

                    fact.Tags!.Remove(tag);

                    var used = unitOfWork.GetRepository<Fact>()
                                          .GetAll(x => x.Tags!.Select(t => t.Name).Contains(tag.Name), true)
                                          .ToArray();

                    if (used.Length == 1)
                    {
                        tagRepository.Delete(tag);
                    }
                }
            }



            foreach (var name in toCreate)
            {
                var tag = await tagRepository.GetFirstOrDefaultAsync(predicate: x => x.Name.ToLower() == name, disableTracking: false);
                if (tag == null)
                {
                    var t = new Tag
                    {
                        Name = name.Trim().ToLower()
                    };
                    await tagRepository.InsertAsync(t, cancellationToken);
                    fact.Tags!.Add(t);
                }
                else
                {
                    fact.Tags!.Add(tag);
                }
            }
        }
    }
    public interface ITagService
    {
        public Task<List<TagCloudViewModel>> GetClouds();
        public Task ProcessTagsAsync(IHaveTags viewModel, Fact fact, CancellationToken cancellationToken);
    }
}


