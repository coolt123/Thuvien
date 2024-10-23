namespace ThuvienMvc.Dtos.BorrowingDtos
{
    public class EditBorrowingDto
    {
        public int Idbor { get; set; }
        public DateTime Startat { get; set; }
        public DateTime Endat { get; set; }
        public DateTime? ActualEndAt { get; set; }
        public List<int> bookIds { get; set; }
    }

}
