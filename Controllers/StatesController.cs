using Microsoft.AspNetCore.Mvc;
using ArkApplication.Framework.NoSql;
using ArkApplication.Models;
using System.Collections.Generic;

namespace ArkApplication.Controllers
{

     [Route("api/[controller]")]
     public class StatesController : Controller
     {
 
            #region Filed

            private readonly INoSqlRepository<States> stateRepository;

            #endregion
            
            #region Constr

            public StatesController(INoSqlRepository<States> states)
            {
                stateRepository = states;
            }

            #endregion

            #region Method
       
            [Route("")]
            [HttpGet]
            public IEnumerable<States> Index()
            {
                return stateRepository.Collection();
            }

            #endregion

     }

}