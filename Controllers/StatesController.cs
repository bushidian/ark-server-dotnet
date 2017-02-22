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

            private readonly INoSqlRepository<states> stateRepository;

            #endregion
            
            #region Constr

            public StatesController(INoSqlRepository<states> states)
            {
                stateRepository = states;
            }

            #endregion

            #region Method
       
            [Route("")]
            [HttpGet]
            public IEnumerable<states> Index()
            {
                return stateRepository.Collection();
            }

            #endregion

     }

}