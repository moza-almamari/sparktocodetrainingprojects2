using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Database.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }   
        public string Comment { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
