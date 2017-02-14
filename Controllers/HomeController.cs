using Microsoft.AspNetCore.Mvc;
using System;

namespace ArtApplication.Controllers
{

    public class HomeController: Controller
    {
        [Route("")]
        public string Index()
        {
            return "Hello Word";
        }
        
        [Route("Word")]
        public string Word()
        {
            return "Word";
        }
        
        [Route("api/home/ping")]
        public JsonResult Ping()
        {
            var host = Request.Host.Value;
            var str = string.Format("Host:{0},Time:{1}.", host,
             DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
            return Json(str);
        }

        [Route("Hello")]
        public string Hello()
        {
            return "Hello";
        }
          
    }
}