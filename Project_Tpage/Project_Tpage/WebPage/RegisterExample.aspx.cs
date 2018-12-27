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
            if (!Controller.IsConstrut)
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
            Controller.controller.GetUserInput(ViewOp.Register_register);

            if (PageData.Out.Keys.Contains("failinfo"))
            {
                Label lbl = FindControl("lblInfo") as Label;
                lbl.Visible = true;
                lbl.Text = PageData.Out["failinfo"] as string;
            }
            else
                Response.Redirect("login.aspx");
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            Controller.controller.GetUserInput(ViewOp.Register_back);
            Response.Redirect("login.aspx");
        }
    }
}