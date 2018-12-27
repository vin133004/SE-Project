using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class Article : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Controller.IsConstrut)
            {
                //建構MVC
                Controller.Initial(Session["state"] == null ? StateEnum.Article : (StateEnum)Session["state"]);
            }
        }

        // 返回文章列表
        protected void btnBack_Click(object sender, EventArgs e)
        {
            /*compile目前不能過
            PageData.In.SetData(
                delegate ()
                {
                    //PageData.In["Board"] = name;

                });*/
            Controller.controller.GetUserInput(ViewOp.Article_back);
            Response.Redirect("ArticleList.aspx");
        }

        // 留言
        protected void btnSend_Click(object sender, EventArgs e)
        {

            // 缺少留言格式
            PageData.In.SetData(
                delegate ()
                {
                    //PageData.In["Board"] = name;

                });
            //Controller.controller.GetUserInput(ViewOp.Article_back);
            Controller.controller.ReleaseAMessage(this.Message.Text, this.Title.Text);
            Response.Redirect("Article2.aspx");
        }
    }
}