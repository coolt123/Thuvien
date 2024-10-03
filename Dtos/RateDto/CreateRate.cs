namespace ThuvienMvc.Dtos.RateDto
{
    public class CreateRate
    {
        public int BorrowingId { get; set; }
        public int Star { get; set; }
        public int IdBook { get; set; }
        public int IdUser { get; set; }
    }
}
