using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Tpage.WebPage
{
    public partial class editor : Project_Tpage.Class.View
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Back_Click(object sender, EventArgs e)
        {

            this.controller.ToHome();
            this.model.RequestPageData(controller.model.State);
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
            this.controller.ReleaseArticle(tittle,content,group,board);
            this.model.RequestPageData(controller.model.State);
        }
    }
}