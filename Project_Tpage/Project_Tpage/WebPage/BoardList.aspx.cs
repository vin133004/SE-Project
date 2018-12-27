using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class BoardList : System.Web.UI.Page
    {
        // 資料庫在這邊給資料。顯示於listbox
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Controller.IsConstrut)
            {
                //建構MVC
                Controller.Initial(Session["state"] == null ? StateEnum.Board : (StateEnum)Session["state"]);
            }
        }

        // 選擇要看文章
        protected void SelectArticle(object sender, EventArgs e)
        {
            int index = this.ListOfBoard.SelectedIndex;
            Controller.controller.GetUserInput(ViewOp.Board_viewarticle);
            Response.Redirect("ArticleList.aspx");
        }
    }
}