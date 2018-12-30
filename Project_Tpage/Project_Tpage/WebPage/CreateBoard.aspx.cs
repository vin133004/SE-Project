using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class CreateBoard : System.Web.UI.Page
    {
        // 創建看板
        public event ViewEventHandler DoCreate;
        // 邀請成員
        public event ViewEventHandler DoInvite;
        // 返回首頁
        public event ViewEventHandler ToHome;

        /// <summary>
        /// 每次事件後取得的輸出結果。
        /// </summary>
        public DAT optDAT;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPo_Click(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void btnInvite_Click(object sender, EventArgs e)
        {

        }

        protected void btnNoInvite_Click(object sender, EventArgs e)
        {

        }
    }
}