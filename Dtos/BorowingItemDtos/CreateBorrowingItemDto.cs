﻿using ThuvienMvc.Models;

namespace ThuvienMvc.Dtos.BorowingItemDtos
{
    public class CreateBorrowingItemDto
    {
        public int IDitem { get; set; }
        public int Quantity { get; set; }
        public int IdBor { get; set; }
        public int IdBook { get; set; }

        //public Book Book { get; set; }
        //public Borrowing Borrowing { get; set; }
    }
}