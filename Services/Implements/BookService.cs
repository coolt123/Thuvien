using ThuvienMvc.Dtos.BookDtos;
using ThuvienMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ThuvienMvc.Models;
using X.PagedList;
using X.PagedList.Extensions;
using static System.Reflection.Metadata.BlobBuilder;

namespace ThuvienMvc.Services.Implements
{
    public class BookService : IBookService
    {
        private readonly Data _service;
        public BookService (Data service)
        {

            _service = service; 
        }
        public void Create(CreateBookDto input)
        {
            var book = new Book
            {
                Title = input.Title,
                SubTitle = input.SubTitle,
                Description = input.Description,
                Image = input.Image,
                PublishingYear = input.PublishingYear,
                QuantityInStock = input.QuantityInStock,    
                Createdat = DateTime.Now,
                Updatedat = DateTime.Now,
                deleteflag = false,
                IdAuthor = input.IdAuthor ,
                IdGenres = input.IdGenres,
            };
           
            _service.Add(book);
            _service.SaveChanges();
        }

        public void Delete(int id)
        {
            var result = _service.books.SingleOrDefault(e => e.IdBook == id);
            if (result == null)
            {
                throw new Exception("Book not found.");
            }

            _service.Remove(result);
            _service.SaveChanges();
            
        }


        public Book GetById(int id)
        {
            var result = _service.books.Include(a=>a.Author).Include(u=>u.Genre).Include(b => b.Rating).ThenInclude(e => e.User).SingleOrDefault(e => e.IdBook == id);
            if (result == null)
            {
                return null;
            }
           
            return result; 
        }


        public void Update(UpdateBookDto input)
        {
           
            var result = _service.books.SingleOrDefault(e => e.IdBook == input.IdBook);
            if (result == null)
            {
                
                throw new Exception("Book not found.");
            }
            result.Title = input.Title;
            result.SubTitle = input.SubTitle;
            result.Description = input.Description;
            result.Image = input.Image;
            result.IdAuthor = input.IdAuthor;
            result.IdGenres = input.IdGenres;
            result.Updatedat = input.Updatedat;
            result.QuantityInStock = input.QuantityInStock;
            result.PublishingYear = input.PublishingYear;
            _service.books.Update(result);
            _service.SaveChanges();

        }

        public IPagedList<Book> Search(string input)
        {
            var books = from b in _service.books select b;
            if (!string.IsNullOrEmpty(input))
            {
                books = books.Where(e => e.Title.Contains(input));
            }
            return books.Include(e => e.Author).Include(e => e.Genre).ToPagedList();
        }
        public IPagedList<Book> GetPagedBooks(string input, int page, int pageSize)
        {
            var books = _service.books.AsQueryable();
            if (!string.IsNullOrEmpty(input))
            {
                books = books.Where(e => e.Title.Contains(input));
            }

            books = books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .OrderBy(b => b.Title);


            return books.ToPagedList(page, pageSize);
        }


        public List<Book> GetAll()
        {
            return _service.books.Include(e => e.Author).Include(e => e.Genre).ToList();
        }

    }
}
