using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZT.Web.Areas.User.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /User/Article/

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
    }
}
