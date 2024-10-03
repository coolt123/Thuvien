using ThuvienMvc.Dtos.BorowingItemDtos;
using ThuvienMvc.Models;

namespace ThuvienMvc.Dtos.BorrowingDtos
{
    public class CreateBorrowingDto
    {
        //class này là người dùng muốn điền vào những thông tin gì ?
        //create thì không điền Id của nó vào nhé vì Id là tự tăng
        public int IdUser { get; set; }
        public DateTime Startat { get; set; }
        public DateTime Endat { get; set; }
        public DateTime? ActualEndAt { get; set; }

        //public ICollection<BorrowingItems> BorrowingItems { get; set; }
        //public User User { get; set; }
        //public int BookId { get; set; }
        // cái này đầu vào của nó có thể là 1 list sách
        public List<int> bookIds { get; set; } //thêm các trường này vào đây nữa
        //từ từ để nghĩ, làm thế này ko quen
        // thêm r đấy nghĩ tiếp đi 

        }
}
