using Microsoft.EntityFrameworkCore;

namespace ArkApplication.Framework.Data
{
    
    public class ArkDbContext : DbContext 
    {

         public ArkDbContext(DbContextOptions<ArkDbContext> options)
             :base(options)
         {

         }

    }
}