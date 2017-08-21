using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System
{
    public class RequestHelper
    {
        #region 判断是否post请求 + static bool IsPostBack()
        /// <summary>
        /// 判断是否post请求
        /// </summary>
        /// <returns></returns>
        public static bool IsPostBack()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("post", StringComparison.CurrentCultureIgnoreCase) ? true : false;
        } 
        #endregion
    }
}
