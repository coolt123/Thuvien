using System.Collections;

namespace ThuvienMvc.Models
{
    public abstract class CRUDbooks
    {
        public DateTime Createdat { get; set; }
        public DateTime Updatedat { get; set; }
        public bool deleteflag { get; set; }
    }
}
