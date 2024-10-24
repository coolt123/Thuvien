﻿using Microsoft.AspNetCore.Authorization;
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
using ThuvienMvc.Services.Interfaces;
using X.PagedList;
using static System.Reflection.Metadata.BlobBuilder;

namespace ThuvienMvc.Controllers
{
    public class BorrowingController : Controller
    {
        private readonly Data _data;
        private readonly IBorrowingService _service; 
        public BorrowingController(Data Data , IBorrowingService service)
        {
            _data = Data;
            _service = service;
        }

        [Authentication]
        public IActionResult Index()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");

            if (UserId == null)
            {
                return RedirectToAction("SignIn", "User");
            }

            var phieumuon = _service.GetBorrowingsByUserId((int)UserId);
            return View(phieumuon);
        }
        [AuthenAdmin]
        public IActionResult IndexAdmin(string name, int page = 1)
        {
           
            
                page = page < 1 ? 1 : page;
                int pageSize = 7;
                IPagedList<Borrowing> borrowings = _service.GetPagedBorrowings(name, page, pageSize);
                if (!string.IsNullOrEmpty(name) && (borrowings == null || !borrowings.Any()))
                {
                    ViewBag.Message = "Không tồn tại người dùng nào theo kết quả tìm kiếm.<a href='javascript:history.back()'>Quay lại</a>";
                }
                ViewData["SearchName"] = name;
                return View(borrowings);
            
        }

        [Authentication]
        public IActionResult Create()
        {
            ViewBag.IdUser = HttpContext.Session.GetInt32("UserId");
            var books = _data.books.Where(b => b.deleteflag == false).Select(b => new {
                b.IdBook,
                b.Title
            }).ToList();

            ViewBag.Sach = books;
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
        [Authentication]
        public IActionResult Edit(int id)
        {
            // Lấy thông tin borrowing từ database
            var borrowing = _data.borrowings.FirstOrDefault(b => b.Idbor == id && b.deleteflag == false);
            if (borrowing == null)
            {
                return NotFound(); // Trả về lỗi nếu không tìm thấy borrowing
            }

            // Lấy danh sách các sách đã được mượn
            var borrowingItems = _data.borrowingItems
                .Where(bi => bi.IdBor == borrowing.Idbor && bi.deleteflag == false)
                .Select(bi => bi.IdBook)
                .ToList();

            // Lấy danh sách tất cả các sách chưa bị xoá
            var books = _data.books
                .Where(b => b.deleteflag == false)
                .Select(b => new {
                    b.IdBook,
                    b.Title
                }).ToList();

            // Khởi tạo ViewBag.Sach
            ViewBag.Sach = books; // Bạn không cần khởi tạo lại SelectList ở đây

            // Khởi tạo model để truyền vào view
            var borrowingDto = new EditBorrowingDto
            {
                Idbor = borrowing.Idbor,
                Startat = borrowing.Startat,
                Endat = borrowing.Endat,
                ActualEndAt = borrowing.ActualEndAt,
                bookIds = borrowingItems // Truyền các sách đã được chọn
            };

            return View(borrowingDto); // Trả về view với model
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Idbor,Startat,Endat,ActualEndAt,bookIds")] EditBorrowingDto input)
        {
            if (ModelState.IsValid)
            {
                var borrowing = _data.borrowings.FirstOrDefault(b => b.Idbor == id && b.deleteflag == false);

                if (borrowing == null)
                {
                    return NotFound();
                }

                // Cập nhật thông tin Borrowing
                borrowing.Startat = input.Startat;
                borrowing.Endat = input.Endat;
                borrowing.ActualEndAt = input.ActualEndAt;
                borrowing.Updatedat = DateTime.Now;

                _data.borrowings.Update(borrowing);

                // Xóa các BorrowingItems cũ
                var oldItems = _data.borrowingItems.Where(bi => bi.IdBor == borrowing.Idbor).ToList();
                if (oldItems.Any())
                {
                    _data.borrowingItems.RemoveRange(oldItems); // Xóa hẳn các mục cũ
                }

                // Thêm các BorrowingItems mới
                if (input.bookIds != null && input.bookIds.Any())
                {
                    var newItems = input.bookIds.Select(bookId => new BorrowingItems
                    {
                        IdBook = bookId,
                        IdBor = borrowing.Idbor,
                        Quantity = 1,
                        Createdat = DateTime.Now,
                        Updatedat = DateTime.Now,
                        deleteflag = false
                    }).ToList();

                    _data.borrowingItems.AddRange(newItems);
                }

                _data.SaveChanges();
                return RedirectToAction("Index");
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
        [HttpPost]    
            public IActionResult delete(int id)
            {
                var borrowing = _data.borrowings.SingleOrDefault(a => a.Idbor == id);
                if (borrowing == null)
                {
                    return NotFound();
                }
            try
            {
               
                _data.borrowings.Remove(borrowing);

                
                _data.SaveChanges();

               
                return RedirectToAction("IndexAdmin");
            }
            catch (Exception ex)
            {
              
                ModelState.AddModelError("", "Không thể xóa bản ghi này.");
                return View(borrowing);
            }
            
            }
        }
    }
