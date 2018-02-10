using System.Web;
using System.Web.Mvc;
using ZYL.Web.Utils;

namespace ZYL.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LocalizationAttribute());
        }
    }
}
