using Microsoft.AspNetCore.Mvc;
using ArkApplication.Framework.Common.Operations;
using ArkApplication.Models;
using ArkApplication.Framework.NoSql;
using System;

namespace ArkApplication.Controllers
{

    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        #region Filed

        private readonly INoSqlRepository<users> usersRepository;

        #endregion

        #region Constr

        public AuthController(INoSqlRepository<users> users)
        {
            usersRepository = users;
        }

        #endregion

        [Route("Login")]
        [HttpPost]
        public OperationResult<users> Login([FromBody] users user)
        {

            user.name = user.email.Substring(0, user.email.IndexOf('@'));

            return new OperationResult<users>(true, user);
        }

        [Route("Logout")]
        [HttpPost]
        public bool Logout()
        {
            return true;
        }

    }


}