using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZT.Api.User.Authorize;

namespace ZT.Web.Areas.User.Controllers
{
    [UserAuthorize]
    public class SectionsController : Controller
    {
        //
        // GET: /User/Sections/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }

        public ActionResult EditContent()
        {
            return View();
        }
    }
}
