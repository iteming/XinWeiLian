using Entity;
using Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XWL.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var obj = new Repository<Admin>().Get().FirstOrDefault();
            return View();
        }
        public ActionResult Register()
        {
            var obj = new Repository<Admin>().Get().FirstOrDefault();
            return View();
        }
    }
}