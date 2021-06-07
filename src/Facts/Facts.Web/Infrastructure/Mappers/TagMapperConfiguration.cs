using AutoMapper;
using Calabonga.UnitOfWork;
using Facts.Web.Data.Dto;
using Facts.Web.Infrastructure.Mappers.Base;
using Facts.Web.ViewModels;
using System;
using System.Collections.Generic;

namespace Facts.Web.Infrastructure.Mappers
{
    public class TagMapperConfiguration : MapperConfigurationBase
    {
        public TagMapperConfiguration()
        {
            CreateMap<Tag, TagViewModel>();
            CreateMap<Tag, TagUpdateViewModel>();
            CreateMap<TagUpdateViewModel, Tag>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Facts, o => o.Ignore());
        }
    }
}
