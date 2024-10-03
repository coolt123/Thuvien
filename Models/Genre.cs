namespace ThuvienMvc.Models
{
    public class Genre : CRUDbooks
    {
        public int IdGenres {  get; set; }
        public string NameGenres {  get; set; }
        public ICollection<Book> Book {  get; set; }
    }
}
