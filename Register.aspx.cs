using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRChat
{
    public partial class Register : System.Web.UI.Page
    {
        ConnClass ConnC = new ConnClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegister_ServerClick(object sender, EventArgs e)
        {
            string Query = "insert into UserData(UID, UserName, Displayname,Email,Password)Values('" + GenerateID() + "','" + txtName.Value + "','" + txtName.Value + "','"+txtEmail.Value+"','"+txtPassword.Value+"')";
            string ExistQ = "select * from UserData where Email='" + txtEmail.Value+"'";
            if (!ConnC.IsExist(ExistQ))
            {
                if (ConnC.ExecuteQuery(Query))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Congratulations!! You have successfully registered..');", true);
                    Session["Username"] = txtName.Value;
                    Session["Email"] = txtEmail.Value;
                    Response.Redirect("Chat.aspx");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Email is already Exists!! Please Try Different Email..');", true);
            }
        } 

        private string GenerateID()
        {
            long currentMilis = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            return (currentMilis%10000000).ToString();
        }
    }
}