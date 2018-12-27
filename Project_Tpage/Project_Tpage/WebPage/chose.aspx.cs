using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class chose : Project_Tpage.Class.View
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Controller.IsConstrut)
            {
                //建構MVC
                Controller.Initial(Session["state"] == null ? StateEnum.Home : (StateEnum)Session["state"]);
            }
            this.Image1.ImageUrl = "./pictures/2p3o0003noq07q391981.jpg";
        }
        protected void Board(object sender, EventArgs e)
        {

            Controller.controller.ToBoard();
            Controller.model.RequestPageData(Controller.model.State);
        }
        protected void group(object sender, EventArgs e)
        {

            Controller.controller.ToGroup();
            Controller.model.RequestPageData(Controller.model.State);
        }
        protected void artic(object sender, EventArgs e)
        {

            Controller.controller.ToEdit();
            Controller.model.RequestPageData(Controller.model.State);
        }
        protected void setting(object sender, EventArgs e)
        {

            Controller.controller.ToUserSetting();
            Controller.model.RequestPageData(Controller.model.State);
        }
    }
}