using ThuvienMvc.Dtos.BookDtos;
using ThuvienMvc.Dtos.BorrowingDtos;
using ThuvienMvc.Models;
using X.PagedList;

namespace ThuvienMvc.Services.Interfaces
{
    public interface IBorrowingService
    {
        public void Add(CreateBorrowingDto input);
        IPagedList<Borrowing> GetPagedBorrowings(string name, int page, int pageSize);
        List<Borrowing> GetBorrowingsByUserId(int userId);
    }
}
