using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class editor : Project_Tpage.Class.View
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Controller.IsConstrut)
            {
                //建構MVC
                Controller.Initial(Session["state"] == null ? StateEnum.EditArticle : (StateEnum)Session["state"]);
            }
        }
        protected void Back_Click(object sender, EventArgs e)
        {

        }
        protected void Setting_Click(object sender, EventArgs e)
        {

           // this.controller.;
           // this.model.RequestPageData(controller.model.State);
        }
        protected void Send_Click(object sender, EventArgs e)
        {
            String content = Content.Text;
            String tittle = Tittle.Text;
            String group = Group.Text;
            String board = Board.Text;
        }
    }
}