using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GGGETSAdmin.Common;
using IMMENSITY.SWFUploadAPI;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

public partial class SWFUpload_upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string cmd = SWFUrlOper.GetStringParamValue("cmd");
            if (cmd == string.Empty)
                this.uploadFile();
            else if (cmd == "del")
                this.DelFile();
        }
    }

    public void uploadFile()
    {
        //保存的路径
        string savePath = SWFUrlOper.GetFormStringParamValue("path");
        //旧文件名,方便删除
        string oldFileName = SWFUrlOper.GetFormStringParamValue("fn");
        //是否需要小图
        bool isSmall = SWFUrlOper.GetFormStringParamValue("small").ToLower() == "true" ? true : false;
        //是否需要水印
        bool isWaterMark = SWFUrlOper.GetFormStringParamValue("wm").ToLower() == "true" ? true : false;
        //小图宽度
        int smallWidth = SWFUrlOper.GetFormIntParamValue("sw");
        //小图高度
        int smallHeight = SWFUrlOper.GetFormIntParamValue("sh");
        //图片扩展名
        string[] imgExtension = new string[] { "jpg", "gif", "png", "bmp" };
        //已上传文件的文件名
        string nameList = SWFUrlOper.GetFormStringParamValue("data");
        try
        {
            // 获取上传的文件信息
            HttpPostedFile file_upload = Request.Files["Filedata"];
            string extension = string.Empty;
            string fileName = string.Empty;
            //bool isImg = false;
            if (file_upload.ContentLength > 0)
            {
                fileName = file_upload.FileName;
                if (fileName.IndexOf(".") != -1)
                    extension = fileName.Substring(fileName.LastIndexOf(".") + 1, fileName.Length - fileName.LastIndexOf(".") - 1).ToLower();

                SWFUploadFile uf = new SWFUploadFile();
                if (isSmall)
                {
                    uf.SmallPic = true;
                    uf.MaxWith = smallWidth == 0 ? uf.MaxWith : smallWidth;
                    uf.MaxHeight = smallHeight == 0 ? uf.MaxHeight : smallHeight;
                }
                uf.IsWaterMark = isWaterMark;
                int state = 0;
                string newFileName = uf.SaveFile(file_upload, savePath, oldFileName, ref state);

                //0:上传成功.  1:没有选择要上传的文件.  2:上传文件类型不符.   3:上传文件过大  -1:应用程序错误.
                int statusCode = 500;
                switch (state)
                {
                    case 0:
                        statusCode = 200;
                        break;
                    case 1:
                        statusCode = 200;
                        break;
                    default:
                        statusCode = 500;
                        break;
                }

                List<SWFUploadFileInfo> listufi = new List<SWFUploadFileInfo>();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<SWFUploadFileInfo>));
                if (nameList != string.Empty)
                    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(nameList)))
                        listufi = (List<SWFUploadFileInfo>)serializer.ReadObject(ms);//反序列化

                #region 得到图片尺寸
                int imgWidth = 0;
                int imgHeight = 0;
                foreach (string item in imgExtension)
                {
                    if (item == extension && file_upload.InputStream != null)
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(file_upload.InputStream);
                        imgWidth = img.Width;
                        imgHeight = img.Height;
                        img.Dispose();
                    }
                }
                #endregion

                listufi.Add(new SWFUploadFileInfo()
                {
                    Id = listufi.Count,
                    FileName = newFileName,
                    OriginalFileName = fileName,
                    Path = savePath,
                    Width = imgWidth,
                    Height = imgHeight
                });
                string postData = "";
                //序列化
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, listufi);
                    postData = Encoding.UTF8.GetString(stream.ToArray());
                }

                Response.StatusCode = statusCode;
                //状态存入cookie
                if (statusCode==200) CookiesHelp.Add("uploadStatus", statusCode.ToString());
                Response.Write(postData);
            }
        }
        catch (Exception e)
        {
            //内部服务器错误
            Response.StatusCode = 500;
            Response.Write("内部服务器错误:" + e.Message.ToString());
        }
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
    /// <summary>
    /// 删除文件
    /// </summary>
    /// <returns></returns>
    public void DelFile()
    {
        string name = SWFUrlOper.GetStringParamValue("name");
        string msg = new SWFUploadFile().Delete(SWFUrlOper.GetStringParamValue("f"), SWFUrlOper.GetStringParamValue("name"), true);
        Response.Write(msg);
        Response.End();
    }
}