using Entity;
using Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Tools;
using Entity.Param;
using ZYL.Web.Utils;

namespace ZYL.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}