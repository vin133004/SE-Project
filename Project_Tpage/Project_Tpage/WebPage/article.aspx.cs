using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class article : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Controller.IsConstrut)
            {
                //建構MVC
                Controller.Initial(Session["state"] == null ? StateEnum.Article : (StateEnum)Session["state"]);
            }
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            
                Controller.controller.GetUserInput(ViewOp.Article_back);
                Response.Redirect("ArticleList.aspx");
            
        }
        protected void Good_Click(object sender, EventArgs e)
        {

           //點讚
        }
        protected void Send_Click(object sender, EventArgs e)
        {
            string message = TextBox1.Text;
            Controller.controller.ReleaseAMessage(message, Tittle.Text);
        }
    }
}