using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Runtime.CompilerServices;

namespace ThuvienMvc.Models
{
    public class Author : CRUDbooks
    {
        public int IdAuthor { get; set; }
        public string NameAuthor { get; set; }
        public ICollection<Book> Book { get; set; }
        
    }
}
