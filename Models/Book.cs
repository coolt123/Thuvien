namespace ThuvienMvc.Models
{
    public class Book : CRUDbooks
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
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Rating> Rating { get; set; }
        public ICollection<BorrowingItems> BorrowingItems { get; set; }

    }
}