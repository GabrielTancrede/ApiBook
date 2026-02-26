using AutoMapper;
using Book.Application.ViewModels.Book;
using Book.Core.Common;
using BookEntity = Book.Core.Entities.Book;

namespace Book.Application.Features.Books.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookEntity, BookViewModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<PagedList<BookEntity>, PagedList<BookViewModel>>();
            CreateMap<BookEntity, ViewModels.Genre.BookSimpleViewModel>();
            CreateMap<BookEntity, ViewModels.Author.BookSimpleViewModel>();
        }
    }
}
