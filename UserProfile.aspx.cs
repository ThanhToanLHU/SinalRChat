using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SD = System.Drawing;
using System.Collections;
using System.Web.Services;
using System.Xml.Linq;

namespace SignalRChat
{
    public partial class UserProfile : System.Web.UI.Page
    {
        public static UserProfile Singleton;

        public string Email = "";
        public string NickName = "";
        public static string UserName = "admin";
        public string UserImage = "/images/DP/dummy.png";

        public string newUserName = "";

        public string NewUserName
        {
            get
            {
                return this.newUserName;
            }
            set
            {
                newUserName = value;
                Response.Redirect("Chat.aspx");
            }
        }

        ConnClass ConnC = new ConnClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                HttpContext.Current = Context;

                Singleton = this;
                UserName = Session["UserName"].ToString();
                GetUserImage(UserName);
                Email = GetUserColData(UserName, "Email");
                NickName = GetUserColData(UserName, "Displayname");

                txtName.Text = NickName;
                txtEmail.Text = Email;
            }
            else
                Response.Redirect("Login.aspx");
        }

        void LoadChatScreen()
        {
            HttpContext.Current.Response.Redirect("Chat.aspx");
        }
        
        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        public void OnBtnSaveClick(object sender, EventArgs e)
        {

            UpdateDataCol(UserName, "Displayname", txtName.Text);
            UpdateDataCol(UserName, "Email", txtEmail.Text);

            
            //Response.Redirect("Chat.aspx");
        }
        protected void btnRedirect_Click(object sender, EventArgs e)
        {
            string script = $"alert('{Singleton.txtEmail.Text}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ButtonClickAlert", script, true);

            Response.Redirect("Chat.aspx");
        }

        protected void OnBtnCloseClick(object sender, EventArgs e)
        {
            //string script = $"alert('{Singleton.txtEmail.Text}');";
            //ClientScript.RegisterStartupScript(this.GetType(), "ButtonClickAlert", script, true);

            //UpdateDataCol(UserName, "Displayname", NickName);
            //UpdateDataCol(UserName, "Email", Email);
            Response.Redirect("Chat.aspx");
        }

        [WebMethod]
        public static void HandleButtonClick()
        {
            //string[] strings = data.Split(',');
            Singleton.NickName = Singleton.txtEmail.Text;
            Singleton.txtEmail.Text = "a";
            //Email = strings[1];
        }

      

        public void GetUserImage(string Username)
        {
            if (Username != null)
            {
                string query = "select Avatar from UserData where UserName='" + Username + "'";

                string ImageName = ConnC.GetColumnVal(query, "Avatar");
                if (ImageName != "")
                    UserImage = "images/DP/" + ImageName;
            }
        }

        public string GetUserColData(string Username, string colData)
        {
            string dataReturn = "";
            if (Username != null)
            {
                string query = $"select {colData} from UserData where UserName='" + Username + "'";

                dataReturn = ConnC.GetColumnVal(query, colData);
                
            }

            return dataReturn;
        }

        protected void btnChangePicModel_Click(object sender, EventArgs e)
        {

            string serverPath = HttpContext.Current.Server.MapPath("~/");
            //path = serverPath + path;
            if (FileUpload1.HasFile)
            {
                string FileWithPat = serverPath + @"images/DP/" + UserName + FileUpload1.FileName;

                FileUpload1.SaveAs(FileWithPat);
                SD.Image img = SD.Image.FromFile(FileWithPat);
                SD.Image img1 = RezizeImage(img, 151, 150);
                img1.Save(FileWithPat);
                if (File.Exists(FileWithPat))
                {
                    FileInfo fi = new FileInfo(FileWithPat);
                    string ImageName = fi.Name;
                    if (UpdateDataCol(UserName, "Avatar", ImageName))
                        UserImage = "images/DP/" + ImageName;
                }
            }
        }

        public bool UpdateDataCol(string Username, string colData, string data)
        {
            string query = "update UserData set "+ colData + " ='" + data + "' where UserName='" + Username + "'";
            return ConnC.ExecuteQuery(query);
        }

        #region Resize Image With Best Qaulity

        private SD.Image RezizeImage(SD.Image img, int maxWidth, int maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                Bitmap cpy = new Bitmap(nnx, nny, SD.Imaging.PixelFormat.Format32bppArgb);
                using (Graphics gr = Graphics.FromImage(cpy))
                {
                    gr.Clear(Color.Transparent);

                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    gr.DrawImage(img,
                        new Rectangle(0, 0, nnx, nny),
                        new Rectangle(0, 0, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                return cpy;
            }

        }

        private MemoryStream BytearrayToStream(byte[] arr)
        {
            return new MemoryStream(arr, 0, arr.Length);
        }

        #endregion

    }
}