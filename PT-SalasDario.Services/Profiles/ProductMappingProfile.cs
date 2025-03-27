using AutoMapper;
using PT_SalasDario.Data;
using PT_SalasDario.Services.Requests;

namespace PT_SalasDario.Services.Profiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductFromCSV, Product>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
           .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.ToString()))
           .ForMember(dest => dest.CategoryCode, opt => opt.MapFrom(src => src.CategoryCode.ToString()))
           .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
           .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category
           {
               Id = Guid.NewGuid(),
               Code = src.CategoryCode.ToString(),
               Name = src.CategoryName,
               CreationDate = DateTime.UtcNow
           }));
        }
    }
}
