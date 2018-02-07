using Entity;
using Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Tools;
using Entity.Param;
using XWL.Web.Utils;

namespace XWL.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SubmitRegister(string name, string email, string phone, string walletAddress)
        {
            var ret = new ResultModel();
            var entity = new User();
            var rep = new Repository<User>();

            #region 验证
            entity = rep.Get(f => f.Email.Contains(email)).FirstOrDefault();
            if (entity != null)
            {
                ret = "邮箱已存在".SetResult(null);
                return Json(ret, JsonRequestBehavior.AllowGet);
            }

            entity = rep.Get(f => f.Phone.Contains(phone)).FirstOrDefault();
            if (entity != null)
            {
                ret = "手机号已存在".SetResult(null);
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            #endregion

            entity = new User
            {
                Name = name,
                Email = email,
                Phone = phone,
                WalletAddress = walletAddress,
                RegisterTime = DateTime.Now
            };

            if (rep.Insert(entity) > 0)
                return Json("注册成功".SetResult(true), JsonRequestBehavior.AllowGet);
            else
                return Json("注册失败".SetResult(null), JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult Login()
        {
            var username = Request["username"];
            var password = Request["password"];
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var rep = new Repository<Admin>();
                var entity = rep.Get(f => f.UserName == username.Trim() && f.Password == password)
                    .FirstOrDefault();
                if (entity != null)
                {
                    Utils.Common.Session.UserInfo = entity;
                    return RedirectToAction("UserList");
                }
                ViewBag.TipTitle = "帐号或密码错误!";
            }
            return View();
        }


        [SessionUserFilter]
        public ActionResult UserList()
        {
            return View();
        }
        
        /// <summary>
        /// PageIndex = param.page,
        /// PageSize = param.rows
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUserList(ParamUserList param)
        {
            var rep = new Repository<User>();

            var queryable = rep.Get();
            if (!string.IsNullOrEmpty(param.keywords))
                queryable = queryable.Where(f => f.Name.Contains(param.keywords) ||
                                                 f.Email.Contains(param.keywords) ||
                                                 f.Phone.Contains(param.keywords));

            if (!string.IsNullOrEmpty(param.BeginTime))
            {
                var beginTime = Convert.ToDateTime(param.BeginTime);
                queryable = queryable.Where(f => f.RegisterTime >= beginTime);
            }
                
            
            if (!string.IsNullOrEmpty(param.EndTime))
            {
                var endTime = Convert.ToDateTime(param.EndTime);
                queryable = queryable.Where(f => f.RegisterTime <= endTime);
            }

            var count = queryable.Count();
            var data = queryable.OrderByDescending(f=> f.RegisterTime)
                .Skip((param.page - 1) * param.rows).Take(param.rows).ToList();

            var result = ConstClass.Success.SetResultPager(data, count, param.page, param.rows);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ResultModel UserDelete(List<int> ids)
        {
            try
            {
                var rep = new Repository<User>();
                var list = rep.Get(f => ids.Contains(f.UserId)).ToList();
                if (list.Count > 0)
                {
                    rep.Delete(list);
                    return ConstClass.Success.SetResult(ConstClass.Success);
                }
                return ConstClass.Failed.SetResult(null);
            }
            catch (Exception)
            {
                return ConstClass.Exception.SetResult(null);
            }
        }
    }
}