using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZYL.Web.Utils
{
    public class SessionUserFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (null == Common.Session.UserInfo || string.IsNullOrEmpty(Common.Session.UserInfo.UserName))
            {
                if (filterContext.ActionDescriptor.GetCustomAttributes(new Common.NoSessionAuth().GetType(), false).Length == 0)
                {
                    string returnUrl = filterContext.HttpContext.Request.Url.AbsolutePath;
                    if ("/" == returnUrl || string.IsNullOrEmpty(returnUrl))
                    {
                        base.OnActionExecuting(filterContext);
                        return;
                    }

                    string redirectUrl = Resources.Global.Lang + "/Home/Index";
                    var isAjax = filterContext.HttpContext.Request.IsAjaxRequest();
                    if (!isAjax)
                    {
                        filterContext.HttpContext.Response.Redirect(redirectUrl);
                        return;
                    }
                    else
                    {
                        ActionResult ret = new HttpStatusCodeResult(System.Net.HttpStatusCode.RequestTimeout);
                        filterContext.Result = ret;
                        return;
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}