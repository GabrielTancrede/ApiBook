using AutoMapper;
using Book.Application.ViewModels.Genre;
using Book.Core.Entites;

namespace Book.Application.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreViewModel>();
            
            CreateMap<Genre, GenreWithBooksViewModel>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
        }
    }
}
