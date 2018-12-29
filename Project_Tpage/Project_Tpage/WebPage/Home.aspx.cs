using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;

namespace Project_Tpage.WebPage
{
    public partial class Home : System.Web.UI.Page
    {
        //  進去看板頁面
        public event ViewEventHandler ToBoard;

        //  進去廣告頁面
        public event ViewEventHandler ToAD;

        //  查詢看板
        public event ViewEventHandler DoSearch;

        //  申請看板
        public event ViewEventHandler DoCreateBoard;

        //  抽卡
        public event ViewEventHandler DoCard;

        //  更改樣式
        public event ViewEventHandler DoStyle;

        //  事件的輸出結果
        public DAT optDAT;

        //  初始化
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //  點選不同看板進入
        protected void SelectBoard(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["Board"] = ListOfBoard.SelectedItem.Text;
        }
        
        //  開/關申請選項
        protected void btnNew_Click(object sender, EventArgs e)
        {
            boardText.Visible = !boardText.Visible;
            btnBoard.Visible = !btnBoard.Visible;
        }

        //  抽卡,回傳資訊顯示於cardInfo
        protected void btnCard_Click(object sender, EventArgs e)
        {

        }

        //  送出查詢資料
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
        }


        //  前往廣告頁面
        protected void btnAD_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
        }
            
        //  送出申請看板,接收是否成功訊息
        protected void btnBoard_Click(object sender, EventArgs e)
        {
            DAT dat = new DAT();
            dat["Board"] = boardText.Text;
        }

        //  更改顯示樣式
        protected void StyleChanged(object sender, EventArgs e)
        {
           int i= StyleList.SelectedIndex;
            if (i == 0) {
                color.Style.Add("background-color", "lightblue");
            }
            else
            {
                color.Style.Add("background-color", "dimgray");
            }
        }

        protected void yes_Click(object sender, EventArgs e)
        {
         
        }
    }
}