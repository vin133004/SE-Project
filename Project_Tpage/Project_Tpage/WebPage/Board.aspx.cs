using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class Board : System.Web.UI.Page
    {
        // 前往文章
        public event ViewEventHandler ToArticle;
        // 前往PO文
        public event ViewEventHandler ToEditor;
        // 返回
        public event ViewEventHandler ToBack;
        // 追隨
        public event ViewEventHandler DoFollow;
        // 邀請成員(版主or管理者)
        public event ViewEventHandler DoInvite;
        // 邀請為管理者(版主or管理者)(可以外部or現有成員)
        public event ViewEventHandler DoAdmin;
        // 刪除成員(版主or管理者)
        public event ViewEventHandler DoDelPeople;
        // 刪除看板(版主)
        public event ViewEventHandler DoDelBoard;

        /// <summary>
        /// 每次事件後取得的輸出結果。
        /// </summary>
        public DAT optDAT;

        // 資料庫在這邊給資料。顯示於listbox
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // 選擇要看文章
        protected void SelectArticle(object sender, EventArgs e)
        {

        }

        // 返回看板
        protected void btnBack_Click(object sender, EventArgs e)
        {

 
        }

        // 發新文章
        protected void btnPo_Click(object sender, EventArgs e)
        {

        }

        //  刪除成員(版主or管理者) Info 回傳到 peopleInfo
        protected void btnDel_Click(object sender, EventArgs e)
        {

        }
        //  邀請成為管理者(版主or管理者) Info 回傳到 peopleInfo
        protected void btnAdmin_Click(object sender, EventArgs e)
        {

        }
        //  邀請加入(版主or管理者) Info 回傳到 peopleInfo
        protected void btnInvite_Click(object sender, EventArgs e)
        {

        }
        // 刪除看板(僅限版主)
        protected void btnDelBoard_Click(object sender, EventArgs e)
        {

        }
    }
}