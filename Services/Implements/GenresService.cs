using ThuvienMvc.Dtos.AuthorDto;
using ThuvienMvc.Dtos.BookDtos;
using ThuvienMvc.Dtos.GenreDto;
using ThuvienMvc.Models;
using ThuvienMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace ThuvienMvc.Services.Implements
{
    public class GenresService : IGenreservice
    {
        private readonly Data _servicce;
        public GenresService(Data servicce)
        {
            _servicce = servicce;
        }
        public List<Genre> GetAll()
        {
            return _servicce.genres.ToList();
        }
        public IEnumerable<Genre> Get()
        {
            return _servicce.genres.ToList();
        }

        public GenreDto GetById(int id)
        {
            var genre = _servicce.genres.SingleOrDefault(e => e.IdGenres == id);
            if (genre == null)
            {
                return null;
            }
            var result = new GenreDto
            {
               IdGenres= id,
               NameGenres = genre.NameGenres,

            };
            return result;
        }

        public void Delete(int id)
        {
            var genre = _servicce.genres.SingleOrDefault(e=>e.IdGenres==id);
            if (genre == null)
            
            {
                throw new Exception("");
            }

            _servicce.Remove(genre);
            _servicce.SaveChanges();
        }

        public void Create(CreateGenre input)
        {
            var genre = new Genre
            {
                NameGenres = input.NameGenres,
                Createdat = DateTime.Now,
                Updatedat = DateTime.Now,
                deleteflag = false,
            };
            _servicce.genres.Add(genre);
            _servicce.SaveChanges();
        }

        public void Update(GenreDto input)
        {
           

                var result = _servicce.genres.SingleOrDefault(e => e.IdGenres == input.IdGenres);
                if (result == null)
                {

                    throw new Exception("Genres not found.");
                }
                result.NameGenres = input.NameGenres;
                _servicce.genres.Update(result);
                _servicce.SaveChanges();
            
        }

        public IPagedList<Genre> GetPagedGenres(string name, int page, int pageSize)
        {
            var genres = _servicce.genres.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                genres = genres.Where(e => e.NameGenres.Contains(name));
            }

            return genres.ToPagedList(page, pageSize);
        }
    }
}
