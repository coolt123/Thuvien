using ThuvienMvc.Dtos.BookDtos;
using ThuvienMvc.Dtos.BorrowingDtos;
using ThuvienMvc.Models;

namespace ThuvienMvc.Services.Interfaces
{
    public interface IBorrowingService
    {
        public void Add(CreateBorrowingDto input);
       
    }
}
