using ThuvienMvc.Models;

namespace ThuvienMvc.Dtos.BookDtos
{
    public class BookDto : CRUDbooks
    {
        public int IdBook { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public int? IdAuthor { get; set; }
        public int? IdGenres { get; set; }
        public int? PublishingYear { get; set; }
        public int? QuantityInStock { get; set; }
        
    }
}
