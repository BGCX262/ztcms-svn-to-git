using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZT.Api.User.Authorize;

namespace ZT.Web.Areas.User.Controllers
{
    [UserAuthorize]
    public class UserController : Controller
    {
        //
        // GET: /User/User/

        public ActionResult Index()
        {
            return View();
        }

    }
}
