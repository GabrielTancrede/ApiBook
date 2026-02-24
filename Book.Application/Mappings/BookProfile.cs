using AutoMapper;
using Book.Application.ViewModels.Book;
using BookEntity = Book.Core.Entites.Book;

namespace Book.Application.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookEntity, BookViewModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

            // For simple book view in Genre and Author with books
            CreateMap<BookEntity, ViewModels.Genre.BookSimpleViewModel>();
            CreateMap<BookEntity, ViewModels.Author.BookSimpleViewModel>();
        }
    }
}
