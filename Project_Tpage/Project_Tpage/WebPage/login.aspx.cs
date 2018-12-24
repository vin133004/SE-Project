using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Project_Tpage.WebPage
{
    public partial class login : Project_Tpage.Class.View
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Image1.ImageUrl = "..\\..\\App_Data/pictures/2p3o0003noq07q391981.jpg";
            this.Image1.ImageUrl = "./pictures/2p3o0003noq07q391981.jpg";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            String account = TextBox1.Text;
            String password = TextBox2.Text;
            this.controller.Login(account,password);
            this.model.RequestPageData(controller.model.State);
        }

    }
}