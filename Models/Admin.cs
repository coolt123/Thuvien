using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuvienMvc.Models
{
 
    public class Admin : CRUDbooks
    {
       
        
        public int IdAmin {  get; set; }
        public string NameAdmin { get; set; }
        public string AdminName { get; set; }
        public string Password { get; set; } 
       
    }
}
