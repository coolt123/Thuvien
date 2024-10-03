using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ThuvienMvc.Dtos.BorrowingDtos;
using ThuvienMvc.Models;
using ThuvienMvc.Services.Implements;
using ThuvienMvc.Models.Authentications;

namespace ThuvienMvc.Controllers
{
    public class BorrowingController : Controller
    {
        private readonly Data _data;
        public BorrowingController(Data Data)
        {
            _data = Data;
        }

        [Authentication]
        public IActionResult Index()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");

            var phieumuon = _data.borrowings.Where(e => e.IdUser == UserId).Include(e => e.BorrowingItems).ThenInclude(bi => bi.Book).ToList();
            return View(phieumuon);
        }
        [Authentication]
        public IActionResult Create()
        {
            ViewBag.IdUser = HttpContext.Session.GetInt32("UserId");
            ViewBag.Sach = new SelectList(_data.books.Where(b => b.deleteflag == false), "IdBook", "Title");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdUser,Startat,Endat,ActualEndAt,bookIds")] CreateBorrowingDto input)
        {


            if (ModelState.IsValid)
            {
                if ((input.bookIds != null && input.bookIds.Any()))
                {
                    var borrowing = new Borrowing
                    {
                        Startat = input.Startat,
                        Endat = input.Endat,
                        IdUser = input.IdUser,
                        Createdat = DateTime.Now,
                        Updatedat = DateTime.Now,
                        deleteflag = false
                    };
                    _data.borrowings.Add(borrowing);
                    _data.SaveChanges();

                    var borrowingItems = input.bookIds.Select(bookId => new BorrowingItems
                    {
                        IdBook = bookId,
                        Quantity = 1,
                        Createdat = DateTime.Now,
                        Updatedat = DateTime.Now,
                        deleteflag = false,
                        IdBor = borrowing.Idbor
                    }).ToList();

                    _data.borrowingItems.AddRange(borrowingItems);
                    _data.SaveChanges();
                    return RedirectToAction("Index");
                }
               
            }
             ViewBag.IdUser = HttpContext.Session.GetInt32("UserId");
             ViewBag.Sach = new SelectList(_data.books.Where(b => b.deleteflag == false), "IdBook", "Title");
             return View(input);


        }
            //var borrowingItems = input.bookIds.Select(bookId => new BorrowingItems
            //{
            //    IdBook = bookId,
            //    Quantity = 1,               
            //    Createdat = DateTime.Now,
            //    Updatedat = DateTime.Now,
            //    deleteflag = false,
            //    IdBor = borrowing.Idbor
            //}).ToList();


            //_data.borrowingItems.AddRange(borrowingItems);
            //_data.SaveChanges(); 






            [Authentication]
            [HttpPost]
            public IActionResult ReturnBook(int borrowingId)
            {
                var borrowing = _data.borrowings
                .Include(b => b.BorrowingItems)
                .FirstOrDefault(b => b.Idbor == borrowingId);
                if (borrowing != null)
                {
                    borrowing.deleteflag = true;
                    borrowing.Updatedat = DateTime.Now;
                    borrowing.ActualEndAt = DateTime.Now;
                    _data.SaveChanges();
                }

                return RedirectToAction("Index");
            }


            //    if (ModelState.IsValid)
            //    {
            //        var borrowing = _data.borrowings.Add(new Borrowing
            //        {
            //            ActualEndAt = input.ActualEndAt,
            //            Startat = input.Startat,
            //            IdUser = input.IdUser,
            //            BorrowingItems = input.bookIds.Select(bookId => new BorrowingItems
            //            {
            //                IdBook = bookId,
            //                IdBor = borrowing.Idbor, // ID của borrowing vừa tạo
            //            }).ToList()
            //            // Điền các trường thông tin khác vào đây
            //        }).Entity;

            //        //var borrowingItems = input.bookIds.Select(bookId => new BorrowingItems
            //        //{
            //        //    IdBook = bookId,
            //        //    IdBor = borrowing.Idbor, // ID của borrowing vừa tạo
            //        //}).ToList();

            //        //// Thêm BorrowingItems vào ngữ cảnh
            //        //_data.borrowingItems.AddRange(borrowingItems);

            //        // Lưu các thay đổi vào cơ sở dữ liệu
            //        _data.SaveChanges();

            //        return RedirectToAction("Index"); // Chuyển hướng đến phương thức Index hoặc trang khác
            //    }

            //    return View(input); // Trả về View với thông tin lỗi nếu Model không hợp lệ
            //}
            //public void Add(CreateBorrowingDto input)
            //{
            //    var borrowing = _context.borrowings.Add(new Borrowing
            //    {
            //        ActualEndAt = input.ActualEndAt,
            //        Startat = input.Startat,
            //        //điền các trường thông tin khác vào đây
            //    }).Entity;


            //    var borrowingItems = input.bookIds.Select(bookId => new BorrowingItems
            //    {
            //        IdBook = bookId,
            //        IdBor = borrowing.Idbor,
            //    }).ToList();
            //}

        }
    }
