using Microsoft.AspNetCore.Mvc;
using System;


namespace ArtApplication.Controllers
{
    
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        
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

    }

    public class CustomerEntity 
    {
         public string FirstName { get; set;}

         public string LastName { get; set; }
    }


}