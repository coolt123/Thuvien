using ThuvienMvc.Dtos.BookDtos;
using ThuvienMvc.Services.Implements;
using ThuvienMvc.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ThuvienMvc.Models;
using X.PagedList;
using ThuvienMvc.Dtos.AuthorDto;
using ThuvienMvc.Models.Authentications;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThuvienMvc.Controllers
{
    public class BookController : Controller
    {
        // GET: BookController
        private readonly IBookService _context;
        private readonly IAuthorService _authorService;
        private readonly IGenreservice _genreService;
        private readonly Data _data;
        public BookController(IBookService context , IAuthorService authorService,  IGenreservice genreService , Data data)
        {
            _data = data;
            _context = context;
            _authorService = authorService;
            _genreService = genreService;   
        }
        //[HttpGet]
        /*public ActionResult Index()
        {
            var book = _context.GetAll();
            return View(book);
        }
        */
        [Authentication]
        public ActionResult Index(string name , int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 7;
 

            IPagedList<Book> books = _context.GetPagedBooks(name,page, pageSize);
            ViewData["SearchName"] = name;
            return View(books); 
        }
        //public ActionResult Page(int page = 1)
        //{
        //    page = page < 1 ? 1 : page;
        //    int pagesize = 7;
        //    IPagedList<Book> books = _context.GetPagedBooks(page, pagesize);

        //    return View(books);
        //}

        //[HttpGet]
        //public ActionResult Search(string name , int page = 1)
        //{
        //    page = page < 1 ? 1 : page;
        //    int pageSize = 7;
        //    var books = _context.Search(name);
        //    ViewData["SearchName"] = name;
        //    return View(books);
        //}

        [AuthenAdmin]
        public ActionResult IndexAdmin(string name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 7;


            IPagedList<Book> books = _context.GetPagedBooks(name, page, pageSize);
            ViewData["SearchName"] = name;
            return View(books);
        }

        [AuthenAdmin]
        public ActionResult Edit(int id)
        {
            var bookDto = _context.GetById(id);
            if (bookDto == null)
            {
                return NotFound();
            }
            ViewBag.Authors = new SelectList(_data.authors.ToList(), "IdAuthor", "NameAuthor");
            ViewBag.Genres = new SelectList(_data.genres.ToList(), "IdGenres", "NameGenres");
            return View(bookDto);
        }

        [AuthenAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateBookDto book)
        {
            if (ModelState.IsValid)
            {
                _context.Update(book);
                return RedirectToAction("IndexAdmin"); 
            }
            ViewBag.Authors = new SelectList(_data.authors.ToList(), "IdAuthor", "NameAuthor");
            ViewBag.Genres = new SelectList(_data.genres.ToList(), "IdGenres", "NameGenres");
            return View(book); 
        }

        [AuthenAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var book = _context.GetById(id); 
            if (book == null)
            {
                return NotFound();
            }

            _context.Delete(id); 
            return RedirectToAction("IndexAdmin"); 
        }

        [AuthenAdmin]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Author = new SelectList(_data.authors.Where(b => b.deleteflag == false), "IdAuthor", "NameAuthor");
            ViewBag.Genres = new SelectList(_data.genres.Where(b => b.deleteflag == false), "IdGenres", "NameGenres");
            return View();

        }

        // POST: Books/Create
        [AuthenAdmin]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create([Bind("Title", "IdAuthor" , "IdGenres", "SubTitle", "Image", "Description", "PublishingYear", "QuantityInStock", "IdGenres")]CreateBookDto model)
        {
            if (ModelState.IsValid)
                    {
                        _context.Create(model);
                        return RedirectToAction("IndexAdmin");
                    }


            ViewBag.Author = new SelectList(_data.authors.Where(b => b.deleteflag == false), "IdAuthor", "NameAuthor");
            return View();
            
        }

        [Authentication]
        public IActionResult Detail(int id)
        {
            var book = _data.books
            .Include(b => b.Author)
            .Include(b => b.Genre)
             .Include(b => b.Rating)  
                .ThenInclude(r => r.User) 
            .FirstOrDefault(b => b.IdBook == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
