using Microsoft.AspNetCore.Mvc;

namespace ArkApplication.Controllers
{

    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        [Route("Login")]
        [HttpPost]
        public bool Login()
        {
            return true;
        }

        [Route("Logout")]
        [HttpPost]
        public bool Logout()
        {
            return true;
        }

    }


}