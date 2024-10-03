using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThuvienMvc.Models
{
    public class Borrowing : CRUDbooks
    {
        
        public int Idbor {  get; set; } 
        public int IdUser { get; set; }
        public DateTime Startat { get; set; }
        public DateTime Endat { get; set; }
        public DateTime? ActualEndAt { get; set; }
        public ICollection<BorrowingItems> BorrowingItems { get; set; }
        public User User { get; set; }
        
    }
}
