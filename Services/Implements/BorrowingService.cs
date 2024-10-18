using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using ThuvienMvc.Dtos.BorrowingDtos;
using ThuvienMvc.Models;
using ThuvienMvc.Services.Interfaces;
using X.PagedList;
using X.PagedList.Extensions;

namespace ThuvienMvc.Services.Implements
{
    public class BorrowingService : IBorrowingService
    {
        private readonly Data _context;
        public BorrowingService(Data context)
        {
            _context = context;
        }

        public void Add(CreateBorrowingDto input)
        {
            var borrowing = _context.borrowings.Add(new Borrowing
            {
                ActualEndAt = input.ActualEndAt,
                Startat = input.Startat,
                //điền các trường thông tin khác vào đây
            }).Entity;
                
           
            var borrowingItems = input.bookIds.Select(bookId => new BorrowingItems
            {
                IdBook = bookId,
                IdBor = borrowing.Idbor,
            }).ToList();
        }

        public IPagedList<Borrowing> GetPagedBorrowings(string name, int page, int pageSize)
        {
            IQueryable<Borrowing> borrowing;

            if (!string.IsNullOrEmpty(name))
            {
                borrowing = _context.borrowings
                    .FromSqlRaw(@"SELECT b.Idbor, b.IdUser, b.Startat, b.Endat, b.ActualEndAt, 
                                 u.NameUser 
                          FROM Borrowings b 
                          JOIN Users u ON b.IdUser = u.IdUser
                          WHERE u.NameUser COLLATE SQL_Latin1_General_CP1_CI_AI LIKE {0}", "%" + name + "%")
                    .Include(b => b.User)
                    .AsQueryable();
            }
            else
            {
                
                borrowing = _context.borrowings.Include(b => b.User).AsQueryable();
            }

            return borrowing.ToPagedList(page, pageSize);
        }



        public List<Borrowing> GetBorrowingsByUserId(int userId)
        {
            return _context.borrowings
                           .Where(e => e.IdUser == userId)
                           .Include(e => e.BorrowingItems)
                           .ThenInclude(bi => bi.Book)
                           .ToList();
        }



        //kiểm tra xem cuốn sách đấy có tồn tại hay ko
        //var checkBookId = _context.books.Where(e => e.IdBook == input.BookId);
        //if(checkBookId == null)
        //{
        //    //sách không tồn tại thì trả lỗi về
        //}
        //ở đây không cần else
    }
}
