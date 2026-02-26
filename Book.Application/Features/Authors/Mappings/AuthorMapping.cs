using AutoMapper;
using Book.Application.ViewModels.Author;
using Book.Core.Common;
using Book.Core.Entities;

namespace Book.Application.Features.Authors.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorViewModel>();

            CreateMap<Author, AuthorWithBooksViewModel>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

            CreateMap<PagedList<Author>, PagedList<AuthorViewModel>>();
        }
    }
}
