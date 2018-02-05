﻿using System;
using System.Web;

namespace Common.Tools
{
    public class CookieHelper
    {
        public static CookieHelper _CookieHelper = new CookieHelper();

        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public void ClearCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        public string GetCookieValue(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                str = cookie.Value;
            }
            return str;
        }
        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        public void SetCookie(string cookiename, string cookievalue)
        {
            HttpCookie cookie = new HttpCookie(cookiename);
            cookie.Value = cookievalue;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public void SetCookie(string cookiename, string cookievalue, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookiename)
            {
                Value = cookievalue,
                Expires = expires
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }

    
    public class SessionHelper
    {
        public static SessionHelper _SessionHelper = new SessionHelper();

        public void SetSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
        public object GetSession(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public string UserID
        {
            get
            {
                var userid = HttpContext.Current.Session["UserID"];
                return userid == null ? "" : userid.ToString();
            }
            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }
        }

        public string UserName
        {
            get
            {
                var username = HttpContext.Current.Session["UserName"];
                return username == null ? "" : username.ToString();
            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }
    }
}
