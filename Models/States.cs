using ArkApplication.Framework.NoSql;

namespace ArkApplication.Models
{
  
    public class states : Entity
    {

         public int stateId { get; set; }
          
         public string abbreviation { get; set; }

         public string name { get; set; }

    }  


}