using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Entity.Base;

namespace XWL.Web.Utils
{
    public class SessionExtend
    {
        public static SessionExtend SUPSession = new SessionExtend();
        private const string SESSION_USER = "Session_User";
        private const string SESSION_RIGHT = "Session_Right";

        #region 基础方法
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="supEnumUser"></param>
        /// <returns></returns>
        public string this[EnumUser supEnumUser]
        {
            set
            {
                Dictionary<uint, string> dic = GetValue<Dictionary<uint, string>>("SUP");
                if (null == dic)
                {
                    dic = new Dictionary<uint, string>();
                    CurrentSession.Add("SUP", dic);
                }
                if (dic.ContainsKey((uint)supEnumUser))
                    dic[(uint)supEnumUser] = value;
                else
                    dic.Add((uint)supEnumUser, value);
            }
            get
            {
                string ret = string.Empty;
                Dictionary<uint, string> dic = GetValue<Dictionary<uint, string>>("SUP");
                if (null != dic)
                {
                    if (dic.ContainsKey((uint)supEnumUser))
                        ret = dic[(uint)supEnumUser];
                }
                return ret;
            }
        }

        public void SetValue(string key,object value)
        {
            CurrentSession.Add(key, value);
        }

        public T GetValue<T>(string key)
        {
            T ret = default(T);
            object obj = CurrentSession[key];
            try
            {
                if(null !=obj)
                    ret = (T)obj;
            }
            catch { }
            return ret;
        }

        public object GetValue(string key)
        {
            return CurrentSession[key];
        }
        
        private HttpSessionState CurrentSession
        { 
            get { return HttpContext.Current.Session; }
        }
        #endregion

        /// <summary>
        /// 获取或设置用户信息
        /// </summary>
        public Admin UserInfo
        {
            set
            {
                CurrentSession.Add(SESSION_USER, value);
                SUPSession[EnumUser.UserName ] = value.UserName;
                SUPSession[EnumUser.Id] = value.Id.ToString();
            }
            get { return GetValue<Admin>(SESSION_USER);  }
        }

        /// <summary>
        /// 获取或设置用户权限信息
        /// </summary>
        public List<string> RightInfo
        {
            set { CurrentSession.Add(SESSION_RIGHT, value); }
            get {
                List<string> ret = GetValue<List<string>>(SESSION_RIGHT);
                if (null == ret)
                {
                    ret = new List<string>();
                    CurrentSession.Add(SESSION_RIGHT, ret);
                }
                return ret;
            }
        }
        
        /// <summary>
        /// 检查当前用户是否就有权限
        /// 超级用户具有所有权限
        /// </summary>
        /// <param name="rightCode"></param>
        /// <returns></returns>
        public bool CheckRight(string rightCode)
        {
            bool ret = RightInfo.Contains(rightCode);
            return ret;
        }
        
        public void Clear()
        {
            CurrentSession.Clear();
            CurrentSession.Abandon();
        }

        public void RefreshSession()
        {
        }
    }

    public enum EnumUser : uint
    {
        UserName = 1,
        Id = 2
    }
}