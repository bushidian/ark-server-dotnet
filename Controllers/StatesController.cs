using Microsoft.AspNetCore.Mvc;

namespace ArkApplication.Controllers
{

     [Route("api/[controller]")]
     public class StatesController : Controller
     {

            [Route("")]
            [HttpGet]
            public string Index()
            {
                return "Hello";
            }

     }

}