using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XWL.Web.Utils
{
    public static class Common
    {
        /// <summary>
        /// 公共Session服务
        /// </summary>
        public static SessionExtend Session = SessionExtend.SUPSession;

        /// <summary>
        /// 公共业务日志服务
        /// </summary>
        /// public static LogProvider Log = new LogProvider();
        
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        public static string ClientIp
        {
            get {
                string realRemoteIp = "";
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    realRemoteIp = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0];
                }
                if (string.IsNullOrEmpty(realRemoteIp))
                {
                    realRemoteIp = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrEmpty(realRemoteIp))
                {
                    realRemoteIp = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
                return realRemoteIp;
            }
        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        public static string DomainName
        {
            get
            {
                string realRemoteIp = System.Web.HttpContext.Current.Request.Url.Host.ToString();
                return realRemoteIp;
            }
        }

        /// <summary>
        /// 使用该特性后访问asp.net mvc不需要进行Session超时验证
        /// </summary>
        public class NoSessionAuth : Attribute
        {
            public bool NeedAuth = false;
        }

        /// <summary>
        /// 使用该特性后访问asp.net webAPI不需要进行令牌验证
        /// </summary>
        public class NoTokenAuth : Attribute
        {
            public bool NeedToken = false;
        }
    }
}