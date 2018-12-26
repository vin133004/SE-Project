using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Tpage.WebPage
{
    public partial class ArticleList : System.Web.UI.Page
    {
        // 資料庫在這邊給資料。顯示於listbox
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // 選擇要看文章
        protected void SelectArticle(object sender, EventArgs e)
        {
            int index = this.ListOfArticle.SelectedIndex;
        }
    }
}