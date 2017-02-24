using Microsoft.AspNetCore.Mvc;
using ArkApplication.Framework.Caching;
using ArkApplication.Framework.NoSql;
using ArkApplication.Models;
using ArkApplication.Framework.Common.Operations;
using System.Collections.Generic;
using System.Linq;
using System;


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
            return query.Skip(skip).Take(top);
        }

        [Route("{id}")]
        [HttpGet]
        public customers GetCustomer(string id)
        {
            return customersRepository.GetById(id);
        }

        [Route("")]
        [HttpPost]
        public OperationResult<customers> InsertCustomer([FromBody] customers entity)
        {
            try{
                 var data = customersRepository.Add(entity);
                 return new OperationResult<customers>(true, data);
            }catch(Exception ex){
                 return new OperationResult<customers>(false, null, ex.Message);
            }
           
        }
        
        [Route("{id}")]
        [HttpPut]
        public OperationResult<customers> UpdateCustomer(string id, [FromBody] customers entity)
        {
            try{
                var data = customersRepository.Update(entity);
                return new OperationResult<customers>(true, data);
            }catch(Exception ex){
                return new OperationResult<customers>(false, null , ex.Message);
            }
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