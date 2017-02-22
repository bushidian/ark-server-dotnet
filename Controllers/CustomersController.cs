using Microsoft.AspNetCore.Mvc;
using ArkApplication.Framework.Caching;
using ArkApplication.Framework.NoSql;
using ArkApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace ArkApplication.Controllers
{
    
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {

        #region Constr
 
        #region Filed

        private readonly ICacheManager cacheManger;
        private readonly INoSqlRepository<states> statesRepository;

        private readonly INoSqlRepository<customers> customersRepository;

        #endregion

        public CustomersController(ICacheManager cache, INoSqlRepository<states> states, 
             INoSqlRepository<customers> customers){
            
            cacheManger = cache;
            statesRepository = states;
            customersRepository = customers;
        }

        #endregion 
        
        #region Method

        [Route("")]
        [HttpGet]
        public IEnumerable<customers> Index()
        {
            return customersRepository.AsQueryable().ToList();
        }

        [Route("page/{skip}/{top}")]
        [HttpGet]
        public IEnumerable<customers> GetCustomersPage(int skip=0, int top = 10)
        {
            var query = customersRepository.AsQueryable();
            Response.Headers["X-InlineCount"] = query.Count().ToString();
            return query.Take(top).Skip(skip);
        }

        [Route("{id}")]
        [HttpGet]
        public customers GetCustomer(string id)
        {
            return customersRepository.GetById(id);
        }

        [Route("")]
        [HttpPost]
        public customers InsertCustomer([FromBody] customers entity)
        {
            return customersRepository.Add(entity);
        }
        
        [Route("{id}")]
        [HttpPut]
        public customers UpdateCustomer(string id, customers entity)
        {

            return customersRepository.Update(entity);
        }

        [Route("{id}")]
        [HttpDelete]
        public void DeleteCustomer(string id)
        {
            customersRepository.Delete(id);
        }

        #endregion

    }


}