using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ThuvienMvc.Dtos.BorrowingDtos;
using ThuvienMvc.Models;
using ThuvienMvc.Services.Interfaces;

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

       

      
        //kiểm tra xem cuốn sách đấy có tồn tại hay ko
        //var checkBookId = _context.books.Where(e => e.IdBook == input.BookId);
        //if(checkBookId == null)
        //{
        //    //sách không tồn tại thì trả lỗi về
        //}
        //ở đây không cần else
    }
}
