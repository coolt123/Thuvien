using Microsoft.EntityFrameworkCore;
using ThuvienMvc.Dtos.AuthorDto;
using ThuvienMvc.Models;
using ThuvienMvc.Services.Interfaces;
using X.PagedList;
using X.PagedList.Extensions;

namespace ThuvienMvc.Services.Implements
{
    public class Authorservice : IAuthorService
    {
        private readonly Data _service;
        public Authorservice(Data service)
        {
            _service = service;
        }
        public void Create(CreateAuthorDto input)
        {
            var author = new Author
            {
                NameAuthor = input.NameAuthor,
                Createdat = DateTime.Now,
                Updatedat = DateTime.Now,
                deleteflag = false,
            };
            _service.authors.Add(author);
            _service.SaveChanges();
        }

        public void Delete(int id)
        {
            var result = _service.authors.SingleOrDefault(e=>e.IdAuthor == id); 
            _service.authors.Remove(result);
            _service.SaveChanges();
        }

        public IEnumerable<Author> Get()
        {
            return _service.authors.ToList();
        }

        public List<Author> GetAll()
        {
            return _service.authors.ToList();   
        }

        public AuthorDto GetById(int id)
        {
            var result = _service.authors.SingleOrDefault(e => e.IdAuthor == id);
            if (result == null)
            {
                return null;
            }
            var author = new AuthorDto
            {
                IdAuthor = id,
                NameAuthor = result.NameAuthor,
            };
            return author;
        }

        public IPagedList<Author> GetPagedAuthor(string name, int page, int pageSize)
        {
            var authors = _service.authors.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                authors = authors.Where(e => e.NameAuthor.Contains(name));
            }

            return authors.ToPagedList(page, pageSize);
        }
    

        public void Update(AuthorDto input)
        {

            var result = _service.authors.SingleOrDefault(e => e.IdAuthor == input.IdAuthor);
            if (result == null)
            {

                throw new Exception("Book not found.");
            }
            result.NameAuthor = input.NameAuthor;
            _service.authors.Update(result);
            _service.SaveChanges();
        }
    }
}
