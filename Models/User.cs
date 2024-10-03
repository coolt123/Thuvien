using System.Collections;

namespace ThuvienMvc.Models
{
    public class User : CRUDbooks
    {
        public int IdUser {  get; set; }
        public string NameUser {  get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<Borrowing> Borrowing { get; set; }
        public ICollection<Rating>  Rating { get; set; }
      
    }
}
