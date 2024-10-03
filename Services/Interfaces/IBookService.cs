using ThuvienMvc.Dtos.BookDtos;
using ThuvienMvc.Models;
using X.PagedList;

namespace ThuvienMvc.Services.Interfaces
{
    public interface IBookService
    {
        void Create(CreateBookDto input);
        IPagedList<Book> Search(string input);
        List<Book> GetAll();
        Book GetById(int id);
        void Delete(int id);  
        void Update(UpdateBookDto input);
        IPagedList<Book> GetPagedBooks(string name , int page, int pageSize);

    }
}
