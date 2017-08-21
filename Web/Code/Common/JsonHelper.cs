using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;


namespace System
{
    using System.Web.Security;

    public class JsonHelper
    {
        #region 负责将对象序列化成JSON字符串 + 负责将对象序列化成JSON字符串
        /// <summary>
        /// 负责将对象序列化成JSON字符串
        /// 注意需要引入程序集:System.Web.Extensions
        /// </summary>
        public static string GetJsonStr(object obj)
        {
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            return json.Serialize(obj);
        } 
        #endregion

        #region 将指定的JSON字符串转换为T类型的对象 + static T DeSerialize<T>(string str)
        /// <summary>
        /// 将指定的JSON字符串转换为T类型的对象。
        /// </summary>
        public static T DeSerialize<T>(string str)
        {
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            return json.Deserialize<T>(str);
        } 
        #endregion

        #region 将得到的json字符串转换成object对象 + object DeSerialize(string str)
        /// <summary>
        /// 将得到的json字符串转换成object对象
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static object DeSerialize(string str)
        {
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            return json.DeserializeObject(str);
        }
        #endregion

    }
}
