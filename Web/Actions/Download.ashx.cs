using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.actions
{
    using System.IO;
    using System.Net;

    /// <summary>
    /// Download 的摘要说明
    /// </summary>
    public class Download : IHttpHandler
    {
        #region Request
        private HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }
        #endregion

        #region Response
        private HttpResponse Response
        {
            get
            {
                return HttpContext.Current.Response;
            }
        }
        #endregion

        #region PR方法
        /// <summary>
        /// PR方法
        /// </summary>
        public void ProcessRequest(HttpContext context)
        {
            if (RequestHelper.IsPostBack())//post请求
            {
                DownloadPic();
            }
        }
        #endregion

        #region 下载图片 - void DownloadPic()
        //下载图片
        private void DownloadPic()
        {
            string savePath = HttpUtility.UrlDecode(Request.Params["savePath"]);
            string requestUrl = HttpUtility.UrlDecode(Request.Params["requestUrl"]);
            int min = Convert.ToInt32(Request.Params["min"]);
            int max = Convert.ToInt32(Request.Params["max"]);

            ProcessDownload(savePath, requestUrl, min, max);
        }
        #endregion


        #region 执行下载操作 - void ProcessDownload(string savePath, string requestUrl, int min, int max)
        /// <summary>
        /// 执行下载操作
        /// </summary>
        /// <param name="savePath">图片保存的路径</param>
        /// <param name="requestUrl">请求图片的url地址</param>
        /// <param name="min">从者编号最小值</param>
        /// <param name="max">从者编号最大值</param>
        private void ProcessDownload(string savePath, string requestUrl, int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                string fileName =i.ToString("000");//002
                string[] picArr = new string[] { "A", "B", "C", "D", "E" };
                foreach (var p in picArr)
                {
                    string tmpFileName = fileName;
                    string tmpRequestUrl = requestUrl;
                    string tmpSavePath = savePath;
                    tmpFileName += p + ".png";//002A.png
                    tmpRequestUrl += tmpFileName;//http://file.fgowiki.fgowiki.com/fgo/card/servant/002D.png
                    tmpSavePath += tmpFileName;

                    //WebRequest request = WebRequest.Create(tmpRequestUrl);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(tmpRequestUrl);
                    request.AllowAutoRedirect = false;
                    request.Accept = "*/*";
                    request.Headers.Add("Accept-Language", "zh-cn");
                    request.Headers.Add("Accept-Encoding", "gzip, deflate");
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36";
                    request.KeepAlive = true;
                    request.Timeout = 1000;
                    request.Method = "GET";   

                    request.Timeout = 2000;
                    if (!Directory.Exists(savePath))//不存在则创建
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    try
                    {
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        {

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (Stream stream = response.GetResponseStream()
                           , fsStream = new FileStream(tmpSavePath, FileMode.Create))
                                {
                                    stream.CopyTo(fsStream);//数据流.CopyTo(目标流)
                                }
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        continue;
                    }
                    

                }
            }
            //ReturnMessage();
            AjaxReturnMessage();
        }
        #endregion

        #region 返回消息 - void ReturnMessage()
        /// <summary>
        /// 返回消息
        /// </summary>
        private void ReturnMessage()
        {
            Response.ContentType = "text/html";
            Response.Write("<script>alert('下载完成！');window.location='/index.html'</script>");
            Response.End();
        }
        #endregion

        #region 返回ajax消息 - void AjaxReturnMessage()
        /// <summary>
        /// 返回ajax消息
        /// </summary>
        private void AjaxReturnMessage()
        {
            var msgObj = new
            {
                status = "ok",
                msg = "下载完成！"
            };
            Response.Write(JsonHelper.GetJsonStr(msgObj));
            Response.End();
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}