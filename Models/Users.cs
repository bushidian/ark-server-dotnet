using ArkApplication.Framework.NoSql;

namespace ArkApplication.Models
{

    public class users : Entity
    {
        public string name { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string passwordSalt { get; set; }
    }
}