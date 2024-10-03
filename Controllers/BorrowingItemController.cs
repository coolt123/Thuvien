using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ThuvienMvc.Models;
using ThuvienMvc.Models.Authentications;

namespace ThuvienMvc.Controllers
{
    public class BorrowingItemController : Controller
    {
        private readonly Data _context;
        public BorrowingItemController(Data context)
        {
            _context = context;
        }

        [Authentication]
        public IActionResult Index(int borrowingId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); 
            }
            var result = _context.borrowingItems
           .Include(e => e.Book)             
           .Include(e => e.Borrowing)        
           .Where(e => e.Borrowing.Idbor == borrowingId) 
           .ToList();

            return View(result);
        }
       
    }
    
}
