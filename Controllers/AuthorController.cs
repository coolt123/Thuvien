using Microsoft.AspNetCore.Mvc;
using ThuvienMvc.Services.Interfaces;
using ThuvienMvc.Dtos.AuthorDto;
using ThuvienMvc.Models.Authentications;
using ThuvienMvc.Models;
using X.PagedList;

namespace LibraryMVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _context;
        public AuthorController(IAuthorService context)
        {
            _context = context;
        }

        [AuthenAdmin]
        public ActionResult Index(string name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 7;


            IPagedList<Author> authors = _context.GetPagedAuthor(name, page, pageSize);
            ViewData["SearchName"] = name;
            return View(authors);
        }

        [AuthenAdmin]
        public ActionResult Create()
        {
            var model = new CreateAuthorDto(); 
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAuthorDto model)
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


        [AuthenAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthorDto author)
        {
            if (ModelState.IsValid)
            {
                _context.Update(author);
                return RedirectToAction("Index");
            }
            return View(author);
        }

    }
}
