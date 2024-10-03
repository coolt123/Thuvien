namespace ThuvienMvc.Models
{
    public class Rating : CRUDbooks
    {
        public int IdRate { get; set; }
        public int Star { get; set; }
        public int IdBook { get; set; }
        public int IdUser { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }

    }
}
