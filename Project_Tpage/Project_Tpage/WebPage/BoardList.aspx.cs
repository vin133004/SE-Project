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
<<<<<<< HEAD
            int index = this.ListOfBoard.SelectedIndex;
            Controller.controller.GetUserInput(ViewOp.Board_viewarticle);
            Response.Redirect("ArticleList.aspx");
=======
            //將註冊的資料(輸入參數)寫入PageData.In
            PageData.In.SetData(
                delegate ()
                {
                    PageData.In["Board"] = (FindControl("ListOfBoard") as ListBox).SelectedItem.Text;
                });

            Controller.controller.GetUserInput(ViewOp.Board_viewarticle);

            if (PageData.Out.Keys.Contains("failinfo"))
            {
                Label lbl = FindControl("lblInfo") as Label;
                lbl.Visible = true;
                lbl.Text = PageData.Out["failinfo"] as string;
            }
            else
                Response.Redirect("ArticleList.aspx");
>>>>>>> 8121cb08ff19d42cc620e1f56476230e59b053ec
        }
    }
}