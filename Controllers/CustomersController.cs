using Microsoft.AspNetCore.Mvc;
using ArkApplication.Framework.Caching;
using ArkApplication.Framework.NoSql;
using ArkApplication.Models;

namespace ArkApplication.Controllers
{
    
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        
 
        #region Constr
 
        #region Filed

        private readonly ICacheManager cacheManger;
        private readonly INoSqlRepository<States> statesRepository;

        #endregion

        public CustomersController(ICacheManager cache, INoSqlRepository<States> states){
            
            cacheManger = cache;
            statesRepository = states;
            
        }

        #endregion 
        
        #region Method

        [Route("")]
        [HttpGet]
        public string Index()
        {
            return "Hello";
        }

        [Route("page/{top}/{skip}")]
        [HttpGet]
        public string GetCustomersPage(int top = 10, int skip=0)
        {
            return string.Format("{0}/{1}", top,skip);
        }

        [Route("{id}")]
        [HttpGet]
        public string GetCustomer(string id)
        {
            return string.Format("{0}", id);
        }

        [Route("")]
        [HttpPost]
        public CustomerEntity InsertCustomer([FromBody] CustomerEntity entity)
        {
            return entity;
        }
        
        [Route("{id}")]
        [HttpPut]
        public string UpdateCustomer(string id)
        {
            return string.Format("{0}", id);
        }

        [Route("{id}")]
        [HttpDelete]
        public string DeleteCustomer(string id)
        {
            return string.Format("{0}", id);
        }

        #endregion

    }

    public class CustomerEntity 
    {
         public string FirstName { get; set;}

         public string LastName { get; set; }
    }


}