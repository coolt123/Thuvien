using ThuvienMvc.Dtos.AuthorDto;
using ThuvienMvc.Dtos.BookDtos;
using ThuvienMvc.Models;
using X.PagedList;

namespace ThuvienMvc.Services.Interfaces
{
    public interface IAuthorService
    {
        void Create(CreateAuthorDto input);
        List<Author> GetAll();
        IEnumerable<Author> Get();
        AuthorDto GetById(int id);
        void Delete(int id);
        void Update(AuthorDto input);
        IPagedList<Author> GetPagedAuthor(string name, int page, int pageSize);
    }
}
