using ArkApplication.Framework.NoSql;
using System.Collections.Generic;

namespace ArkApplication.Models
{

    public class Customer : Entity 
    {

        public string FristName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int StateId { get; set;}

        public States States { get; set; }

        public int Zip { get; set; }

        public string Gender { get; set; }

        public int OrderCount { get; set; }

        public IEnumerable<Order> Orders { get; set; }

    }

    public class Order 
    {
        public string Product { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}