using ThuvienMvc.Dtos.GenreDto;
using ThuvienMvc.Models;
using X.PagedList;

namespace ThuvienMvc.Services.Interfaces
{
    public interface IGenreservice
    {
        List<Genre> GetAll();
        IEnumerable<Genre> Get();
        GenreDto GetById(int id);
        void Delete(int id);
        void Create(CreateGenre input);
        void Update(GenreDto input);
        IPagedList<Genre> GetPagedGenres(string name, int page, int pageSize);
    }
}
