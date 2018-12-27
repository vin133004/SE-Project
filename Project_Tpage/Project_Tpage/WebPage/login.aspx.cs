using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class login : Project_Tpage.Class.View
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Image1.ImageUrl = "..\\..\\App_Data/pictures/2p3o0003noq07q391981.jpg";
            this.Image1.ImageUrl = "./pictures/2p3o0003noq07q391981.jpg";

            if (!Controller.IsConstrut)
            {
                //建構MVC
                Controller.Initial(Session["state"] == null ? StateEnum.Login : (StateEnum)Session["state"]);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            String account = TextBox1.Text;
            String password = TextBox2.Text;
            Controller.controller.Login(account, password);
            Controller.model.RequestPageData(Controller.model.State);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Controller.controller.GetUserInput(ViewOp.Login_toregister);
            Response.Redirect("RegisterExample.aspx");
        }
    }
}
