using AutoMapper;
using Book.Application.ViewModels.Author;
using Book.Core.Entites;

namespace Book.Application.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorViewModel>();
            
            CreateMap<Author, AuthorWithBooksViewModel>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
        }
    }
}
