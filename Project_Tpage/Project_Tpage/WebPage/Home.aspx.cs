using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Tpage.Class;
using System.Drawing;
namespace Project_Tpage.WebPage
{
    public partial class Home : System.Web.UI.Page
    {
        //  進去看板頁面
        public event ViewEventHandler ToBoard;
        
        //  進去申請看板頁面
        public event ViewEventHandler ToCreateBoard;

        //  進去廣告頁面
        public event ViewEventHandler ToAD;

        //  查詢看板
        public event ViewEventHandler DoSearch;

        //  抽卡
        public event ViewEventHandler DoCard;

        //  更改樣式
        public event ViewEventHandler DoStyle;

        //  事件的輸出結果
        public DAT optDAT;

        //  初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            color.Style.Add("background-color", "lightblue");



            Color a = Color.Black;
            String border = "white";
            Title.ForeColor = a;
            searchText.ForeColor = a;
            StyleList.ForeColor = a;
            StyleInfo.ForeColor = a;
            btnSearch.ForeColor = a;
            btnNew.ForeColor = a;
            btnAD.ForeColor = a;
            searchresult.ForeColor = a;
            btnCard.ForeColor = a;
            cardInfo.ForeColor = a;
            ListOfBoard.ForeColor = a;
            btnAD.Style.Add("background-color", border);
            btnCard.Style.Add("background-color", border);
            btnNew.Style.Add("background-color", border);
            btnSearch.Style.Add("background-color", border);
            searchText.Style.Add("background-color", border);
            ListOfBoard.Style.Add("background-color", border);
            boardText.Style.Add("background-color", border);
            Panel0.Style.Add("background-color", border);
            Panel1.Style.Add("background-color", border);
            Panel2.Style.Add("background-color", border);
            Panel3.Style.Add("background-color", border);
            StyleList.Style.Add("background-color", border);

            Title.Style.Add("position", "absolute");
            Title.Style.Add("top", "10px");
            Title.Style.Add("left", "45%");

            searchText.Style.Add("position", "absolute");
            searchText.Style.Add("top", "70px");
            searchText.Style.Add("left", "32.5%");

            btnSearch.Style.Add("position", "absolute");
            btnSearch.Style.Add("top", "70px");
            btnSearch.Style.Add("left", "65%");

            searchresult.Style.Add("position", "absolute");
            searchresult.Style.Add("top", "80px");
            searchresult.Style.Add("left", "72.5%");

            ListOfBoard.Style.Add("position", "absolute");
            ListOfBoard.Style.Add("top", "120px");
            ListOfBoard.Style.Add("left", "10%");

            AD.Style.Add("position", "absolute");
            AD.Style.Add("top", "120px");
            AD.Style.Add("left", "55%");

            btnNew.Style.Add("position", "absolute");
            btnNew.Style.Add("top", "450px");
            btnNew.Style.Add("left", "5%");

            btnCard.Style.Add("position", "absolute");
            btnCard.Style.Add("top", "450px");
            btnCard.Style.Add("left", "25%");

            cardInfo.Style.Add("position", "absolute");
            cardInfo.Style.Add("top", "475px");
            cardInfo.Style.Add("left", "35%");

            btnAD.Style.Add("position", "absolute");
            btnAD.Style.Add("top", "450px");
            btnAD.Style.Add("left", "50%");

            StyleInfo.Style.Add("position", "absolute");
            StyleInfo.Style.Add("top", "475px");
            StyleInfo.Style.Add("left", "62.5%");

            StyleList.Style.Add("position", "absolute");
            StyleList.Style.Add("top", "475px");
            StyleList.Style.Add("left", "70%");

            Panel0.Style.Add("position", "absolute");
            Panel0.Style.Add("top", "450px");
            Panel0.Style.Add("left", "80%");

            Panel1.Style.Add("position", "absolute");
            Panel1.Style.Add("top", "530px");
            Panel1.Style.Add("left", "80%");

            Panel2.Style.Add("position", "absolute");
            Panel2.Style.Add("top", "610px");
            Panel2.Style.Add("left", "80%");

            Panel3.Style.Add("position", "absolute");
            Panel3.Style.Add("top", "690px");
            Panel3.Style.Add("left", "80%");

            boardText.Style.Add("position", "absolute");
            boardText.Style.Add("top", "540px");
            boardText.Style.Add("left", "20%");
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
            int stylevalue = StyleList.SelectedIndex;
            if (stylevalue == 0)
            {
                color.Style.Add("background-color", "lightblue");

              

                Color a = Color.Black;
                String border = "white";
                Title.ForeColor = a;
                searchText.ForeColor = a;
                StyleList.ForeColor = a;
                StyleInfo.ForeColor = a;
                btnSearch.ForeColor = a;
                btnNew.ForeColor = a;
                btnAD.ForeColor = a;
                searchresult.ForeColor = a;
                btnCard.ForeColor = a;
                cardInfo.ForeColor = a;
                ListOfBoard.ForeColor = a;
                btnAD.Style.Add("background-color", border);
                btnCard.Style.Add("background-color", border);
                btnNew.Style.Add("background-color", border);
                btnSearch.Style.Add("background-color", border);
                searchText.Style.Add("background-color", border);
                ListOfBoard.Style.Add("background-color", border);
                boardText.Style.Add("background-color", border);
                Panel0.Style.Add("background-color", border);
                Panel1.Style.Add("background-color", border);
                Panel2.Style.Add("background-color", border);
                Panel3.Style.Add("background-color", border);
                StyleList.Style.Add("background-color", border);

                Title.Style.Add("position", "absolute");
                Title.Style.Add("top", "10px");
                Title.Style.Add("left", "45%");

                searchText.Style.Add("position", "absolute");
                searchText.Style.Add("top", "70px");
                searchText.Style.Add("left", "32.5%");

                btnSearch.Style.Add("position", "absolute");
                btnSearch.Style.Add("top", "70px");
                btnSearch.Style.Add("left", "65%");

                searchresult.Style.Add("position", "absolute");
                searchresult.Style.Add("top", "80px");
                searchresult.Style.Add("left", "72.5%");

                ListOfBoard.Style.Add("position", "absolute");
                ListOfBoard.Style.Add("top", "120px");
                ListOfBoard.Style.Add("left", "10%");

                AD.Style.Add("position", "absolute");
                AD.Style.Add("top", "120px");
                AD.Style.Add("left", "55%");

                btnNew.Style.Add("position", "absolute");
                btnNew.Style.Add("top", "450px");
                btnNew.Style.Add("left", "5%");

                btnCard.Style.Add("position", "absolute");
                btnCard.Style.Add("top", "450px");
                btnCard.Style.Add("left", "25%");

                cardInfo.Style.Add("position", "absolute");
                cardInfo.Style.Add("top", "475px");
                cardInfo.Style.Add("left", "35%");

                btnAD.Style.Add("position", "absolute");
                btnAD.Style.Add("top", "450px");
                btnAD.Style.Add("left", "50%");

                StyleInfo.Style.Add("position", "absolute");
                StyleInfo.Style.Add("top", "475px");
                StyleInfo.Style.Add("left", "62.5%");

                StyleList.Style.Add("position", "absolute");
                StyleList.Style.Add("top", "475px");
                StyleList.Style.Add("left", "70%");

                Panel0.Style.Add("position", "absolute");
                Panel0.Style.Add("top", "450px");
                Panel0.Style.Add("left", "80%");

                Panel1.Style.Add("position", "absolute");
                Panel1.Style.Add("top", "530px");
                Panel1.Style.Add("left", "80%");

                Panel2.Style.Add("position", "absolute");
                Panel2.Style.Add("top", "610px");
                Panel2.Style.Add("left", "80%");

                Panel3.Style.Add("position", "absolute");
                Panel3.Style.Add("top", "690px");
                Panel3.Style.Add("left", "80%");

                boardText.Style.Add("position", "absolute");
                boardText.Style.Add("top", "540px");
                boardText.Style.Add("left", "20%");
            }
            else if (stylevalue == 1)
            {
                color.Style.Add("background-color", "black");
                Color a = Color.WhiteSmoke;
                String border = "DarkGray";
                Title.ForeColor = a;
                searchText.ForeColor = a;
                StyleList.ForeColor = a;
                StyleInfo.ForeColor = a;
                btnSearch.ForeColor = a;
                btnNew.ForeColor = a;
                btnAD.ForeColor = a;
                searchresult.ForeColor = a;
                btnCard.ForeColor = a;
                ListOfBoard.ForeColor = a;
                cardInfo.ForeColor = a;
                btnAD.Style.Add("background-color", border);
                btnCard.Style.Add("background-color", border);
                btnNew.Style.Add("background-color", border);
                btnSearch.Style.Add("background-color", border);
                searchText.Style.Add("background-color", border);
                ListOfBoard.Style.Add("background-color", border);
                boardText.Style.Add("background-color", border);
                Panel0.Style.Add("background-color", border);
                Panel1.Style.Add("background-color", border);
                Panel2.Style.Add("background-color", border);
                Panel3.Style.Add("background-color", border);
                StyleList.Style.Add("background-color", border);

                Title.Style.Add("position", "absolute");
                Title.Style.Add("top", "10px");
                Title.Style.Add("left", "45%");

                searchText.Style.Add("position", "absolute");
                searchText.Style.Add("top", "70px");
                searchText.Style.Add("left", "32.5%");

                btnSearch.Style.Add("position", "absolute");
                btnSearch.Style.Add("top", "70px");
                btnSearch.Style.Add("left", "65%");

                searchresult.Style.Add("position", "absolute");
                searchresult.Style.Add("top", "80px");
                searchresult.Style.Add("left", "72.5%");

                ListOfBoard.Style.Add("position", "absolute");
                ListOfBoard.Style.Add("top", "120px");
                ListOfBoard.Style.Add("left", "10%");

                AD.Style.Add("position", "absolute");
                AD.Style.Add("top", "120px");
                AD.Style.Add("left", "55%");

                btnNew.Style.Add("position", "absolute");
                btnNew.Style.Add("top", "450px");
                btnNew.Style.Add("left", "5%");

                btnCard.Style.Add("position", "absolute");
                btnCard.Style.Add("top", "450px");
                btnCard.Style.Add("left", "25%");

                cardInfo.Style.Add("position", "absolute");
                cardInfo.Style.Add("top", "475px");
                cardInfo.Style.Add("left", "35%");

                btnAD.Style.Add("position", "absolute");
                btnAD.Style.Add("top", "450px");
                btnAD.Style.Add("left", "50%");

                StyleInfo.Style.Add("position", "absolute");
                StyleInfo.Style.Add("top", "475px");
                StyleInfo.Style.Add("left", "62.5%");

                StyleList.Style.Add("position", "absolute");
                StyleList.Style.Add("top", "475px");
                StyleList.Style.Add("left", "70%");

                Panel0.Style.Add("position", "absolute");
                Panel0.Style.Add("top", "450px");
                Panel0.Style.Add("left", "80%");

                Panel1.Style.Add("position", "absolute");
                Panel1.Style.Add("top", "530px");
                Panel1.Style.Add("left", "80%");

                Panel2.Style.Add("position", "absolute");
                Panel2.Style.Add("top", "610px");
                Panel2.Style.Add("left", "80%");

                Panel3.Style.Add("position", "absolute");
                Panel3.Style.Add("top", "690px");
                Panel3.Style.Add("left", "80%");

                boardText.Style.Add("position", "absolute");
                boardText.Style.Add("top", "540px");
                boardText.Style.Add("left", "20%");

               
            }
            else
            {

                color.Style.Add("background-color", "BurlyWood");
                Color a = Color.BlueViolet;
                String border = "CadetBlue";
                Title.ForeColor = a;
                searchText.ForeColor = a;
                StyleList.ForeColor = a;
                StyleInfo.ForeColor = a;
                btnSearch.ForeColor = a;
                btnNew.ForeColor = a;
                btnAD.ForeColor = a;
                searchresult.ForeColor = a;
                btnCard.ForeColor = a;
                ListOfBoard.ForeColor = a;
                cardInfo.ForeColor = a;
                btnAD.Style.Add("background-color", border);
                btnCard.Style.Add("background-color", border);
                btnNew.Style.Add("background-color", border);
                btnSearch.Style.Add("background-color", border);
                searchText.Style.Add("background-color", border);
                ListOfBoard.Style.Add("background-color", border);
                boardText.Style.Add("background-color", border);
                Panel0.Style.Add("background-color", border);
                Panel1.Style.Add("background-color", border);
                Panel2.Style.Add("background-color", border);
                Panel3.Style.Add("background-color", border);
                StyleList.Style.Add("background-color", border);

                Title.Style.Add("position", "absolute");
                Title.Style.Add("top", "10px");
                Title.Style.Add("left", "45%");

                searchText.Style.Add("position", "absolute");
                searchText.Style.Add("top", "70px");
                searchText.Style.Add("left", "35%");

                btnSearch.Style.Add("position", "absolute");
                btnSearch.Style.Add("top", "70px");
                btnSearch.Style.Add("left", "65%");

                searchresult.Style.Add("position", "absolute");
                searchresult.Style.Add("top", "80px");
                searchresult.Style.Add("left", "72.5%");

                ListOfBoard.Style.Add("position", "absolute");
                ListOfBoard.Style.Add("top", "120px");
                ListOfBoard.Style.Add("left", "35%");

                AD.Style.Add("position", "absolute");
                AD.Style.Add("top", "450px");
                AD.Style.Add("left", "35%");

                btnNew.Style.Add("position", "absolute");
                btnNew.Style.Add("top", "0px");
                btnNew.Style.Add("left", "0%");

                btnCard.Style.Add("position", "absolute");
                btnCard.Style.Add("top", "80px");
                btnCard.Style.Add("left", "0%");

                cardInfo.Style.Add("position", "absolute");
                cardInfo.Style.Add("top", "100px");
                cardInfo.Style.Add("left", "7.5%");

                btnAD.Style.Add("position", "absolute");
                btnAD.Style.Add("top", "160px");
                btnAD.Style.Add("left", "0%");

                StyleInfo.Style.Add("position", "absolute");
                StyleInfo.Style.Add("top", "240px");
                StyleInfo.Style.Add("left", "0%");

                StyleList.Style.Add("position", "absolute");
                StyleList.Style.Add("top", "270px");
                StyleList.Style.Add("left", "0%");

                Panel0.Style.Add("position", "absolute");
                Panel0.Style.Add("top", "450px");
                Panel0.Style.Add("left", "80%");

                Panel1.Style.Add("position", "absolute");
                Panel1.Style.Add("top", "530px");
                Panel1.Style.Add("left", "80%");

                Panel2.Style.Add("position", "absolute");
                Panel2.Style.Add("top", "610px");
                Panel2.Style.Add("left", "80%");

                Panel3.Style.Add("position", "absolute");
                Panel3.Style.Add("top", "690px");
                Panel3.Style.Add("left", "80%");

                boardText.Style.Add("position", "absolute");
                boardText.Style.Add("top", "540px");
                boardText.Style.Add("left", "20%");


            }
        }


        protected void yes_Click(object sender, EventArgs e)
        {
         
        }
    }
}