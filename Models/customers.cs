using ArkApplication.Framework.NoSql;
using System.Collections.Generic;

namespace ArkApplication.Models
{

    public class customers : Entity 
    {

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string address { get; set; }

        public string city { get; set; }

        public int stateId { get; set;}

        public states state { get; set; }

        public int zip { get; set; }

        public string gender { get; set; }

        public int orderCount { get; set; }

        public IEnumerable<order> orders { get; set; }

    }

    public class order : Entity
    {
        public string product { get; set; }

        public decimal price { get; set; }

        public decimal quantity { get; set; }
    }
}