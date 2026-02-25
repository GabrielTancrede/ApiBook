using AutoMapper;
using Book.Application.ViewModels.Genre;
using Book.Core.Common;
using Book.Core.Entites;

namespace Book.Application.Features.Genres.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreViewModel>();

            CreateMap<Genre, GenreWithBooksViewModel>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

            CreateMap<PagedList<Genre>, PagedList<GenreViewModel>>();
        }
    }
}
