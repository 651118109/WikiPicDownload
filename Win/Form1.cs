using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Win
{
    using System.IO;
    using System.Net;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtSavePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                this.txtSavePath.Text = folder.SelectedPath; 
            }
            if ( this.txtSavePath.Text!="E:/WikiPic/")
            {
                this.txtSavePath.Text += @"\";
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string savePath = txtSavePath.Text.Trim();
            string requestUrl = "http://file.fgowiki.fgowiki.com/fgo/card/servant/";
            int min = Convert.ToInt32(txtMin.Text.Trim());
            int max = Convert.ToInt32(txtMax.Text.Trim());

            if (string.IsNullOrEmpty(savePath))
            {
                MessageBox.Show("请选择保存路径");
                return;
            }

            //匿名委托
            System.Threading.Thread th = new System.Threading.Thread(() =>
            {
                ProcessDownload(savePath, requestUrl, min, max);
            });
            th.IsBackground = true;
            th.Start();

        }

        #region 获得文件名 - string GetFileName(int num)
        /// <summary>
        /// 获得文件名
        /// </summary>
        /// <param name="num">从者编号</param>
        private string GetFileName(int num)
        {
            int len = num.ToString().Length;
            switch (len)
            {
                case 1:
                    return "00" + num;
                case 2:
                    return "0" + num;
                default:
                    return num.ToString();
            }
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
                string fileName = GetFileName(i);//002
                string[] picArr = new string[] { "A", "B", "C", "D", "E" };
                foreach (var p in picArr)
                {
                    string tmpFileName = fileName;
                    string tmpRequestUrl = requestUrl;
                    string tmpSavePath = savePath;
                    tmpFileName += p + ".jpg";//002A.jpg
                    tmpRequestUrl += tmpFileName;//http://file.fgowiki.fgowiki.com/fgo/card/servant/002D.jpg
                    tmpSavePath += tmpFileName;

                    WebRequest request = WebRequest.Create(tmpRequestUrl);

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
            MessageBox.Show("下载完成!");
        }
        #endregion
    }
}
