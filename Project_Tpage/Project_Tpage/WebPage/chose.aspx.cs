using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Tpage.WebPage
{
    public partial class chose : Project_Tpage.Class.View
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Image1.ImageUrl = "./pictures/2p3o0003noq07q391981.jpg";
        }
        protected void Board(object sender, EventArgs e)
        {

            this.controller.ToBoard();
            this.model.RequestPageData(controller.model.State);
        }
        protected void group(object sender, EventArgs e)
        {

            this.controller.ToGroup();
            this.model.RequestPageData(controller.model.State);
        }
        protected void artic(object sender, EventArgs e)
        {

            this.controller.ToEdit();
            this.model.RequestPageData(controller.model.State);
        }
        protected void setting(object sender, EventArgs e)
        {

            this.controller.ToUserSetting();
            this.model.RequestPageData(controller.model.State);
        }
    }
}