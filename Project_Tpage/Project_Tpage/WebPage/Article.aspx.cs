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
        // 發送留言
        public event ViewEventHandler DoMessage;
        // 點讚
        public event ViewEventHandler DoLike;
        // 刪除文章
        public event ViewEventHandler DoDelArticle;
        // 編輯文章
        public event ViewEventHandler ToEdit;
        // 返回看板
        public event ViewEventHandler ToBack;
        // 返回首頁
        public event ViewEventHandler ToHome;
        /// <summary>
        /// 每次事件後取得的輸出結果。
        /// </summary>
        public DAT optDAT;
        //  初始化
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //  返回看板
        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        //  留言
        protected void btnSend_Click(object sender, EventArgs e)
        {

        }

        //  傳送點讚事件
        protected void btnLike_Click(object sender, ImageClickEventArgs e)
        {

        }
        //  返回首頁
        protected void btnHome_Click(object sender, EventArgs e)
        {

        }

        // 刪除文章
        protected void btnDel_Click(object sender, EventArgs e)
        {

        }
        // 編輯文章
        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }
    }
}