using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Database.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        public Review Review { get; set; }
    }
}
