using Microsoft.AspNetCore.Mvc;
using System;


namespace ArtApplication.Controllers
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