using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ThuvienMvc.Dtos.RateDto;
using ThuvienMvc.Models;
using ThuvienMvc.Models.Authentications;

namespace ThuvienMvc.Controllers
{
    public class RatingController : Controller
    {
        private readonly Data _context;
        public RatingController (Data context)
        {
            _context = context;
        }

        [Authentication]
        [HttpPost]
        public IActionResult Create(CreateRate input)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("SignIn", "User");
            }
            var newRating = new Rating
            {
                Star = input.Star,
                IdBook = input.IdBook,
                IdUser = (int)userId , 
                Createdat = DateTime.Now , 
                Updatedat = DateTime.Now ,
                     
            };
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    
                    _context.ratings.Add(newRating);
                    _context.SaveChanges();

                   
                    var borrowingItems = _context.borrowingItems
                                         .Where(bi => bi.IdBook == input.IdBook && bi.Borrowing.IdUser == (int)userId)
                                         .ToList();

                    
                    if (borrowingItems.Any())
                    {
                        foreach (var borrowingItem in borrowingItems)
                        {
                            borrowingItem.deleteflag = true;
                            borrowingItem.Updatedat = DateTime.Now;
                            
                        }
                        _context.borrowingItems.UpdateRange(borrowingItems);
                        _context.SaveChanges();
                    }

                    
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi
                    transaction.Rollback();
                    // Ghi log lỗi hoặc xử lý tùy theo yêu cầu
                    return StatusCode(500, "Internal server error.");
                }

                return RedirectToAction("Index", "Borrowing");
            }
        }
    }
}
