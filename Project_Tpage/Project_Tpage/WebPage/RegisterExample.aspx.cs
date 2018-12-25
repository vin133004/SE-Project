using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class RegisterExample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack && Controller.controller == null)
            {
                //建構MVC
                Controller.Initial(Session["state"] == null ? StateEnum.Login : (StateEnum)Session["state"]);
            }
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            //將註冊的資料(輸入參數)寫入PageData.In
            PageData.In.SetData(
                delegate ()
                {
                    PageData.In["ID"] = (FindControl("tbx1") as TextBox).Text;
                    PageData.In["Password"] = (FindControl("tbx2") as TextBox).Text;
                    PageData.In["Email"] = (FindControl("tbx3") as TextBox).Text;
                    PageData.In["StudentNum"] = (FindControl("tbx4") as TextBox).Text;
                });

            //呼叫Controller方法，切換狀態，要求頁面資料

            Response.Redirect("login.aspx");
        }
    }
}