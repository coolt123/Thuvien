using ThuvienMvc.Models;

namespace ThuvienMvc.Dtos.BookDtos
{
    public class UpdateBookDto :  BookDto 
    {
        public int IdBook { get; set; }
    }
}
