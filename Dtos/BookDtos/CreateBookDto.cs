using ThuvienMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace ThuvienMvc.Dtos.BookDtos
{
    public class CreateBookDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = " không được bỏ trống Title")]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " không được bỏ trống Image")]
        public string Image { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " không được bỏ trống SubTitle")]
        public string SubTitle { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " không được bỏ trống Description")]
        public string Description { get; set; }
        public int? IdAuthor { get; set; }
        
        public int? PublishingYear { get; set; }
        public int? QuantityInStock { get; set; }
        [Required(ErrorMessage = "At least one genre must be selected.")]
        public int? IdGenres { get; set; }
    }
}
