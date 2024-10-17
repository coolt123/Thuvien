using ThuvienMvc.Dtos.AuthorDto;
using ThuvienMvc.Dtos.GenreDto;
using ThuvienMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Azure;
using ThuvienMvc.Models;
using X.PagedList;
using ThuvienMvc.Models.Authentications;

namespace ThuvienMvc.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreservice _context;
        public GenresController(IGenreservice context)
        {
            _context = context;
        }

        [AuthenAdmin]
        public IActionResult Index(string name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 7;


            IPagedList<Genre> genres = _context.GetPagedGenres(name, page, pageSize);
            if (!string.IsNullOrEmpty(name) && (genres == null || !genres.Any()))
            {
                ViewBag.Message = "Không tồn tại thể loại nào theo kết quả tìm kiếm.";
            }
            ViewData["SearchName"] = name;
            return View(genres);
        }

        [AuthenAdmin]
        public ActionResult Create()
        {
            var model = new CreateGenre();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGenre model)
        {
            if (ModelState.IsValid)
            {

                _context.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [AuthenAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var author = _context.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Delete(id);
            return RedirectToAction("Index");
        }

        [AuthenAdmin]
        public ActionResult Edit(int id)
        {
            var result = _context.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GenreDto genre)
        {
            if (ModelState.IsValid)
            {
                _context.Update(genre);
                return RedirectToAction("Index");
            }
            return View(genre);
        }
    }
}
