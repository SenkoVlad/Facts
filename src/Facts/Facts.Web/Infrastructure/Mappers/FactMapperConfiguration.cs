using Calabonga.UnitOfWork;
using Facts.Web.Data.Dto;
using Facts.Web.Infrastructure.Mappers.Base;
using Facts.Web.ViewModels;

namespace Facts.Web.Infrastructure.Mappers
{
    public class FactMapperConfiguration : MapperConfigurationBase
    {
        public FactMapperConfiguration()
        {
            CreateMap<Fact, FactViewModel>();

            CreateMap<FactCreateViewModel, Fact>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Tags, o => o.Ignore())
                .ForMember(x => x.CreatedAt, o => o.Ignore())
                .ForMember(x => x.CreatedBy, o => o.Ignore())
                .ForMember(x => x.UpdatedAt, o => o.Ignore())
                .ForMember(x => x.UpdatedBy, o => o.Ignore());

            CreateMap<Fact, FactEditViewModel>()
                .ForMember(x => x.ReturnUrl, o => o.Ignore())
                .ForMember(x => x.TotalTags, o => o.MapFrom(x => x.Tags == null ? 0 : x.Tags.Count));

            CreateMap<FactEditViewModel, Fact>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Tags, o => o.Ignore())
                .ForMember(x => x.CreatedAt, o => o.Ignore())
                .ForMember(x => x.CreatedBy, o => o.Ignore())
                .ForMember(x => x.UpdatedAt, o => o.Ignore())
                .ForMember(x => x.UpdatedBy, o => o.Ignore());

            CreateMap<IPagedList<Fact>, IPagedList<FactViewModel>>()
                .ConvertUsing<PagedListConverter<Fact, FactViewModel>>();
        }
    }
}
